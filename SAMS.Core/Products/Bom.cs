using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using SAMS.Accessories;
namespace SAMS.Products
{
    [Table("t_bd_bom")]
    public class Bom:Entity
    {
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public virtual string ProductId { get; set; }

        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }
        public virtual string AccessoryId { get; set; }



    }
}
