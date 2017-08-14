using Abp.Domain.Entities;
using SAMS.Accessories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory
{
    [Table("t_inv_Inventory")]
    public class Inventory : Entity
    {
        public virtual long UserId { get; set; }
        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }
        public virtual int AccessoryId { get; set; }

        public virtual int Count { get; set; }

    }
}
