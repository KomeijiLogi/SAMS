using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMS.Areas
{
    [Table("T_BD_Area")]
    public class Area : FullAuditedEntity
    {
        public const int MaxNumberLength = 50;
        public const int MaxNameLength = 50;
        public const int MaxDescriptionLength = 2048;

        [StringLength(MaxNumberLength)]
        public virtual string Number { get; set; }

        [ForeignKey("ParentId")]
        public virtual Area ParentArea { get; set; }
        public virtual int ParentId { get; set; }

    }
}
