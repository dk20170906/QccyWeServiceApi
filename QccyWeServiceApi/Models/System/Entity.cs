using EdaSample.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccyWeServiceApi.Models
{
    public abstract class Entity : IEntity
    {

        public Entity()
        {
            this.Id = Guid.NewGuid();
            this.TimeStamp = DateTime.Now;
        }
        public Entity(Guid guid)
        {
            this.Id = guid;
            this.TimeStamp = DateTime.Now;
        }
        public Guid Id { get; }
        public DateTime TimeStamp { get;  }
        public DateTime UpdateTimeStamp { get; set; }
        public string Remark { get; set; }


    }
}
