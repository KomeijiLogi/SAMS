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
    public class Product : FullAuditedEntity
    {
        public virtual string Name { get; set; }

        public virtual string Model { get; set; }
        public virtual string EASNumber{get;set;}
        public virtual string K3Number { get; set; }
    }
}
