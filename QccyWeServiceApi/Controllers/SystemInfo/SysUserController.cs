using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdaSample.Common.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QccyWeServiceApi.EF;
using QccyWeServiceApi.Models;
using QccyWeServiceApi.Models.System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QccyWeServiceApi.Controllers.SystemInfo
{
    [Route("api/[controller]")]
    public class SysUserController : Controller
    {
        private readonly IEventBus eventBus;
        private readonly ILogger logger;
        private readonly WebApiDbContext dbContext;
        private readonly WebApiConfig webApiConfig;


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get ()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get (int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<JsonResult> Post ([FromBody]SysUser sysUser)
        {
            var user = await dbContext.AddAsync<SysUser>(sysUser);
            if (user.State.Equals(EntityState.Added))
            {
                return new JsonResult(new RespResult
                {
                    Code = (int)RespState.SUCCESS,
                    Msg = RespState.SUCCESS.ToString(),
                    Data=user.Entity
                });
            }
            return new JsonResult(new RespResult
            {
                Code = (int)RespState.ERROR,
                Msg = RespState.ERROR.ToString()
            });

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put (int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete (int id)
        {
        }
    }
}
