using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SAMS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.AccessoryInventoryBills
{
    [Table("t_inv_inventoryBill")]
    public class AccessoryInventoryBill:AuditedEntity
    {
        public virtual string Number { get; set; }
        public virtual InventoryBillType BillType { get; set; }

        /// <summary>
        /// 出入库单位（人员）
        /// </summary>
        public virtual string UserOrOrgName { get; set; }

        [ForeignKey("UsedPersonId")]
        public virtual User UsedPerson { get; set; }
        public virtual long? UsedPersonId { get; set; }



    }
}
