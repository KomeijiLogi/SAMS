using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SAMS.WorkOrders
{
    [Table("T_WO_WorkOrderPhoto")]
    public class WorkOrderPhoto: Entity
    {
        [StringLength(255)]
        public virtual string FilePath { get; set; }
        public virtual string FileType { get; set; }
        [ForeignKey("BillId")]
        public virtual WorkOrderBill Bill { get; set; }
        public virtual int BillId { get; set; }
    }
}
