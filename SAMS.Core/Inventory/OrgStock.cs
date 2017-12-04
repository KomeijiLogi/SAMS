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
    [Table("t_inv_OrgStock")]
    public class OrgStock:Entity
    {
       
       
        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }
        public virtual string AccessoryId { get; set; }

        public virtual int Count { get; set; }

    }
}
