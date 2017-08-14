using Abp.Domain.Entities;
using SAMS.Accessories;

using SAMS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory
{
    [Table("t_inv_StaffStock")]
    public class StaffStock:Entity
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual long UserId { get; set; }
        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }
        public virtual int AccessoryId { get; set; }

        public virtual int Count { get; set; }

    }
}
