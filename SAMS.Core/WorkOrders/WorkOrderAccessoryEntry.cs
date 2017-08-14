using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using SAMS.Faults;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

using SAMS.Accessories;

namespace SAMS.WorkOrders
{
    [Table("T_WO_WorkOrderAccessoryEntry")]
    public class WorkOrderAccessoryEntry:Entity
    {
        
        [ForeignKey("BillId")]
        public virtual WorkOrderBill Bill { get; set; }
        public virtual int BillId { get;  set; }

        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }
        public virtual int AccessoryId { get; set; }
        /// <summary>
        ///使用数量
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 回收数量
        /// </summary>
        public virtual int BackCount { get; set; }
        /// <summary>
        /// 新件序号
        /// </summary>
        public virtual string NewSerials { get; set; }
        /// <summary>
        /// 旧件序号
        /// </summary>
        public virtual string OldSerials { get; set; }


    }
}
