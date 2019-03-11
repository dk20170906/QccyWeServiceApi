using Microsoft.EntityFrameworkCore;
using QccyWeServiceApi.Models;
using QccyWeServiceApi.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccyWeServiceApi.EF
{
    public class WebApiDbContext:DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) 
            : base(options)
        {
        }
        public DbSet<SysUser>  SysUsers { get; set; }
        public DbSet<SysMenu> SysMenus { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysRoleMenu> SysRoleMenus { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
    }
}
