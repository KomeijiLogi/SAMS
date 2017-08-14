using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMS.Faults
{
    [Table("T_BD_FaultReasonRel")]
    public class FaultReasonRel:FullAuditedEntity
    {
        [ForeignKey("FaultId")]
        public virtual Fault Fault { get;  set; }
        public virtual int FaultId { get;  set; }

        [ForeignKey("FaultReasonId")]
        public virtual FaultReason FaultReason { get; set; }
        public virtual int FaultReasonId { get; set; }

    }
}
