using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAMS.Faults
{
    [Table("T_BD_FaultReason")]
    public class FaultReason : FullAuditedEntity
    {
        public const int MaxNumberLength = 50;
        public const int MaxNameLength = 50;
        public const int MaxDescriptionLength = 2048;


        [StringLength(MaxNumberLength)]
        public virtual string Number { get; set; }

        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }
    }
}
