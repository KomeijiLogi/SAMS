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

namespace SAMS.WorkOrders
{
    [Table("T_WO_WorkOrderFaultEntry")]
    public class WorkOrderFaultEntry:Entity
    {
        
        [ForeignKey("ParentId")]
        public virtual WorkOrderBill Bill { get; set; }
        public virtual int ParentId { get;  set; }

        [ForeignKey("FaultId")]
        public virtual Fault Fault { get; set; }
        public virtual int FaultId { get; set; }

        [ForeignKey("FaultReasonId")]
        public virtual FaultReason FaultReason { get; set; }
        public virtual int? FaultReasonId { get; set; }

        [ForeignKey("FaultMeasureId")]
        public virtual FaultMeasure FaultMeasure { get; set; }
        public virtual int? FaultMeasureId { get; set; }
        /// <summary>
        /// 故障照片
        /// </summary>
        public virtual ICollection<WorkOrderPhoto> Photos { get; set; }
    }
}
