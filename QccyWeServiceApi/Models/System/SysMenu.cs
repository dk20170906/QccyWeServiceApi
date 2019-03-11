using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccyWeServiceApi.Models
{
    public class SysMenu : Entity
    {
        public Guid PId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Jump { get; set; }
        public List<SysMenu> List { get; set; }
    }
}
