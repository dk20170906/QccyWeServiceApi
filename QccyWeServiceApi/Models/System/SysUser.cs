﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccyWeServiceApi.Models
{
    public class SysUser:Entity
    {
        public SysUser () { }
        public SysUser (Guid userId, string userName)   :base(userId)
        {
            this.UserName = userName;
        }

        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Accounts { get; set; }
        public string Email { get; set; }
        public string EobilePhone { get; set; }
        public string Password { get; set; }
        public string Vercode { get; set; }
        public string Remember { get; set; }
        public string Access_token { get; set; }
    }
}
