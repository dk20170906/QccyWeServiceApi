using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QccyWeServiceApi.Common;
using QccyWeServiceApi.Models;

namespace QccyWeServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly WebApiConfig webApiConfig;
        public TokenController(IOptions<WebApiConfig> options)
        {
            this.webApiConfig = options.Value;
        }
        [HttpGet]
        public async Task<string> GetToken(string username)
        {
            JWTTokenOptions jwtTokenOptions = new JWTTokenOptions(webApiConfig.JWTIssuer, webApiConfig.JWTAudience, webApiConfig.JWTSecurityKey,webApiConfig.JWTExpires);

            //创建用户身份标识
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, username),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "user"),
            };

            //创建令牌
            var token = new JwtSecurityToken(
                issuer: jwtTokenOptions.Issuer,
                audience: jwtTokenOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(webApiConfig.JWTExpires),
                signingCredentials: jwtTokenOptions.Credentials
                );

            string jwtToken =await Task.Run(()=> new JwtSecurityTokenHandler().WriteToken(token));

            return jwtToken;
        }
    }
}