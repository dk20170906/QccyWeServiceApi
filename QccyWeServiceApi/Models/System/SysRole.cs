using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccyWeServiceApi.Models.System
{
    public class SysRole     :Entity
    {
        public SysRole () { }
        public SysRole(Guid  guid,string roleName) : base(guid)
        {
            this.RoleName = roleName;
        }
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }


    }
}
