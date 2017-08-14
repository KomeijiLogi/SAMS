using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Accessories
{
    [Table("t_bd_accessory")]
    public class Accessory:Entity
    {
        public virtual string Name { get; set; }
        public virtual string Model { get; set; }
        public virtual string Unit { get; set; }
        public virtual string Number { get; set; }
    }
}
