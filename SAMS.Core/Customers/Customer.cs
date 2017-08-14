using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using SAMS.Areas;
namespace SAMS.Customers
{
    [Table("T_BD_Customer")]
    public class Customer: FullAuditedEntity
    {
        public const int MaxNumberLength = 50;
        public const int MaxNameLength = 50;
        public const int MaxAddressLength = 100;
        public const int MaxDescriptionLength = 2048;

        [StringLength(MaxNumberLength)]
        public virtual string Number { get; set; }

        [StringLength(MaxNumberLength)]
        public virtual string Number1 { get; set; }

        [StringLength(MaxNumberLength)]
        public virtual string Number2 { get; set; }

        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [StringLength(MaxAddressLength)]
        public virtual string Area { get; set; }//黑龙江省-大庆市-肇州镇

        [StringLength(MaxAddressLength)]
        public virtual string Address { get; set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }
        /// <summary>
        ///联系人
        /// </summary>
        //[StringLength(MaxNameLength)]
        //public virtual string ContactPerson { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string Email { get; set; }




    }
}
