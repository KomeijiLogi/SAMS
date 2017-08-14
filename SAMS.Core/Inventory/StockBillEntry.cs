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
    [Table("T_inv_StockBillEntry")]
    public class StockBillEntry:Entity
    {
        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }
        public virtual int AccessoryId { get; set; }
        public virtual int Count { get; set; }
        [ForeignKey("BillId")]
        public virtual StockBill Bill { get; set; }
        public virtual int BillId { get; set; }
    }
}
