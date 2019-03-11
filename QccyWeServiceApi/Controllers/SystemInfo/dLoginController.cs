using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdaSample.Common.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data.SqlClient;
using EdaSample.Services.Common.Events;
using QccyWeServiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using QccyWeServiceApi.Common;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using QccyWeServiceApi.EF;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QccyWeServiceApi.Models.System;

namespace QccyWeServiceApi.Controllers.SystemInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly IEventBus eventBus;
        private readonly ILogger logger;
        private readonly WebApiDbContext dbContext;
        private readonly WebApiConfig webApiConfig;

        public LoginController(IConfiguration configuration,
            IEventBus eventBus,
            ILogger<LoginController> logger,
         WebApiDbContext dbContext,
         IOptions<WebApiConfig> option)
        {
            this.configuration = configuration;
            this.connectionString = configuration["WebApiConfig:SqlConnectionString"];
            this.eventBus = eventBus;
            this.logger = logger;
            this.dbContext = dbContext;
            this.webApiConfig = option.Value;
        }
        //public void post () {
        //    string msg = "Fsfddsfads";
        //}

        [HttpPost]
        public async Task<JsonResult> Post ( [FromBody] SysUser  sysUser)
        {
            string result = "无数据";
            //if (username == null)
            //{
            //    string str = "请通过帐号或手机号登录";
            //    return await Task.Run(() => JsonConvert.SerializeObject(new { code = 1, msg = str }));
            //}
            ////var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);
            var user = new SysUser()
            {
                UserName = "123456",
                Password = "123456"
            };

            //if (user == null)
            //{
            //    string str = "无此帐号";
            //    return await Task.Run(() => JsonConvert.SerializeObject(new { code = 1, msg = str }));
            //}
            //if (user.Password != password)
            //{
            //    string str = "密码错误";
            //    return await Task.Run(() => JsonConvert.SerializeObject(new { code = 1, msg = str }));
            //}

            return new JsonResult(new RespResult
            {
                Code = (int)RespState.SUCCESS,
                Msg = "用户添加成功",
                Data = user
            });
        }


        // POST api/values
        //[HttpPost]
        //public IActionResult Post(string username, string password)
        //{
        //    if (username == "AngelaDaddy" && password == "123456")
        //    {
        //        // push the user’s name into a claim, so we can identify the user later on.
        //        var claims = new[]
        //        {
        //           new Claim(ClaimTypes.Name, username)
        //       };
        //        //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webApiConfig.JWTSecurityKey));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
        //        /**
        //         * Claims (Payload)
        //            Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

        //            iss: The issuer of the token，token 是给谁的  发送者
        //            audience: 接收的
        //            sub: The subject of the token，token 主题
        //            exp: Expiration Time。 token 过期时间，Unix 时间戳格式
        //            iat: Issued At。 token 创建时间， Unix 时间戳格式
        //            jti: JWT ID。针对当前 token 的唯一标识
        //            除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
        //         * */
        //        var token = new JwtSecurityToken(
        //            issuer: webApiConfig.JWTIssuer,
        //            audience: webApiConfig.JWTAudience,
        //            claims: claims,
        //            expires: DateTime.Now.AddMinutes(webApiConfig.JWTExpires),
        //            signingCredentials: creds);

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token)
        //        });
        //    }

        //    return BadRequest("用户名密码错误");
        //}

    }
}
