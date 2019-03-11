using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccyWeServiceApi.Models.System
{
    public class SysRoleMenu
    {
        [Key]
        public int Id { get; set; }
        public Guid SysRoleId { get; set; }
        public Guid SysMenuId { get; set; }
    }
}
