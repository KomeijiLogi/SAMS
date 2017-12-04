using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Products
{
    [Table("t_bd_product")]
    public class Product : Entity<string>
    {
        public virtual string Name { get; set; }
        public virtual string Model { get; set; }
        public virtual string Code { get; set; }

    }
}
