using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
namespace SAMS.Faults
{
    [Table("T_BD_FaultMeasureRel")]
    public class FaultMeasureRel:FullAuditedEntity
    {
        [ForeignKey("FaultId")]
        public virtual Fault Fault { get; set; }
        public virtual int FaultId { get; set; }

        [ForeignKey("FaultMeasureId")]
        public virtual FaultMeasure FaultMeasure { get; set; }
        public virtual int FaultMeasureId { get; set; }
    }
}
