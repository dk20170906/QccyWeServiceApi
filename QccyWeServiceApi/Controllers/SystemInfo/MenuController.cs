using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdaSample.Common.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QccyWeServiceApi.EF;
using QccyWeServiceApi.Models;
using QccyWeServiceApi.Models.System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QccyWeServiceApi.Controllers.SystemInfo
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly IEventBus eventBus;
        private readonly ILogger logger;
        private readonly WebApiDbContext dbContext;
        private readonly WebApiConfig webApiConfig;

        public MenuController (
            IEventBus eventBus,
            ILogger<LoginController> logger,
         WebApiDbContext dbContext,
         IOptions<WebApiConfig> option)
        {
            this.eventBus = eventBus;
            this.logger = logger;
            this.dbContext = dbContext;
            this.webApiConfig = option.Value;
        }





        // GET: api/<controller>
        [HttpGet]
        public async Task<JsonResult> Get ()
        {

            var data = new List<object>()
                {
                    new
                    {
                        title="主页",
                        icon="layui-icon-home",
                         List=new List<object>()
                         {
                             new
                             {
                                 title="控制台",
                                 jump="/"
                             }                  ,
                             new
                             {
                                 name="homepage1",
                                 title="主页1",
                                 jump="home/homepage1"
                             }      ,  new
                             {
                                 name="homepage1",
                                 title="主页1",
                                 jump="home/homepage1"
                             }
                         }
                    }
                };
            return new JsonResult(new RespResult
            {
                Code = (int)RespState.SUCCESS,
                Msg = "",
                Data = data
            }
                );

        }

        #region 组装菜单树
        private List<SysMenu> GetSysMenutree (List<SysMenu> sysMenus, List<Guid> menuRootId)
        {
            List<SysMenu> menuTree = new List<SysMenu>();
            List<Guid> keyId = new List<Guid>();
            menuRootId.ForEach(m =>
            {
                if (!keyId.Contains(m))
                {
                    keyId.Add(m);
                    var menu = sysMenus.First(um => um.Id == m);
                    GetSysMenutree(sysMenus, menu);
                    menuTree.Add(menu);
                }
            });
            return menuTree;
        }
        private void GetSysMenutree (List<SysMenu> sysMenus, SysMenu sysMenu)
        {
            List<SysMenu> menus = sysMenus.Where(u => u.PId.Equals(sysMenu.Id)).ToList();
            if (menus != null && menus.Count > 0)
            {
                sysMenu.List = menus;
                sysMenu.List.ForEach(m =>
                {
                    GetSysMenutree(sysMenus, m);
                });
            }
        }

        #endregion

        #region 获取菜单集合的根节点id集合


        private List<Guid> GetRootMenuId (List<SysMenu> sysMenus)
        {
            List<Guid> menuRootId = new List<Guid>();
            List<Guid> menuIdList = sysMenus.Select(m => m.Id).ToList();
            sysMenus.ForEach(m =>
            {
                if (!menuIdList.Contains(m.PId) && !menuRootId.Contains(m.PId))
                {
                    menuRootId.Add(m.PId);
                }
                else
                {
                    GetRootMenuId(sysMenus, menuIdList, menuRootId, m.PId);
                }
            });
            return menuRootId;
        }

        private void GetRootMenuId (List<SysMenu> menuList, List<Guid> menuIdList, List<Guid> menuRootId, Guid pid)
        {
            var vpid = menuList.First(m => m.Id == pid).PId;
            if (!menuIdList.Contains(vpid) && !menuRootId.Contains(vpid))
            {
                menuRootId.Add(vpid);
            }
            else
            {
                GetRootMenuId(menuList, menuIdList, menuRootId, vpid);
            }
        }
        #endregion
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get (Guid id)
        {
            Guid userid = Guid.NewGuid();
            logger.LogInformation("获取左侧菜单");
            List<SysMenu> sysMenus = null;
            try
            {
                await Task.Run(() =>
                  {
                      sysMenus = dbContext.SysMenus.Where(m => dbContext.SysRoleMenus
                       .Where(rm => dbContext.SysRoles.Where(r => dbContext.SysUserRoles
                       .Where(ur => ur.SysUserId == userid).Select(urr => urr.SysRoleId).Contains(r.Id))
                         .Select(ru => ru.Id).Contains(rm.SysRoleId))
                       .Select(rrm => rrm.SysMenuId).Contains(m.Id)).ToList();
                  });
                if (sysMenus != null && sysMenus.Count > 0)
                {
                    var menuRootId = GetRootMenuId(sysMenus);
                    var menuTree = GetSysMenutree(sysMenus, menuRootId);
                    return new JsonResult(new RespResult
                    {
                        Code = (int)RespState.SUCCESS,
                        Msg = RespState.SUCCESS.ToString(),
                        Data = menuTree
                    });
                }
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.ERROR,
                    Success = false,
                    Msg = "无数据或此用户暂没有配置相应角色与权限"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.ERROR,
                    Success = false,
                    Msg = ex.Message
                });
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<JsonResult> Post ([FromBody]SysMenu sysMenu)
        {
            try
            {
                var menu = await dbContext.SysMenus.AddAsync(new SysMenu
                {
                    Icon = sysMenu.Icon,
                    Jump = sysMenu.Jump,
                    Name = sysMenu.Name,
                    PId = sysMenu.PId,
                    Remark = sysMenu.Remark,
                    Title = sysMenu.Title
                });
                if (menu.State.Equals(EntityState.Added))
                {
                    return new JsonResult(new RespResult { Code = (int)RespState.SUCCESS, Msg = "菜单添加成功", Data = menu.Entity });
                }
                return new JsonResult(new RespResult { Code = (int)RespState.ERROR, Success = false, Msg = "菜单添加失败" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new RespResult { Code = 300, Success = false, Msg = ex.Message });
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put (Guid id, [FromBody]SysMenu sysMenu)
        {
            try
            {
                var menu = await dbContext.SysMenus.FirstOrDefaultAsync(u => u.Id.Equals(id));
                menu.Icon = sysMenu.Icon;
                menu.Jump = sysMenu.Jump;
                menu.Name = sysMenu.Name;
                menu.PId = sysMenu.PId;
                menu.Remark = sysMenu.Remark;
                menu.Title = sysMenu.Title;
                menu.UpdateTimeStamp = DateTime.Now;
                var entityMenu = dbContext.SysMenus.Update(menu);
                if (entityMenu.State.Equals(EntityState.Modified))
                {
                    return new JsonResult(new RespResult
                    {
                        Code = (int)RespState.SUCCESS,
                        Msg = "菜单修改成功",
                        Data = menu
                    });
                }
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.ERROR,
                    Success = false,
                    Msg = "菜单悠失败"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.ERROR,
                    Success = false,
                    Msg = ex.Message
                });
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete (Guid id)
        {
            try
            {
                var menu = await dbContext.SysMenus.FirstOrDefaultAsync(u => u.Id.Equals(id));
                var entityEenu = dbContext.SysMenus.Remove(menu);
                if (entityEenu.State.Equals(EntityState.Deleted))
                {
                    return new JsonResult(new RespResult
                    {
                        Code = (int)RespState.SUCCESS,
                        Msg = "删除成功",
                    });
                }
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.ERROR,
                    Success = false,
                    Msg = "删除失败，请重试",
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.ERROR,
                    Success = false,
                    Msg = ex.Message,
                });
            }
        }
    }
}
