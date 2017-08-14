using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SAMS.Customers
{
    [Table("T_BD_CustomerLinkMan")]
    public class CustomerLinkMan:Entity
    {
        public const int MaxNumberLength = 50;
        public const int MaxNameLength = 50;
        public const int MaxAddressLength = 100;
        public const int MaxDescriptionLength = 2048;

        /// <summary>
        /// 客户
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual int CustomerId { get; set; }
        /// <summary>
        ///联系人
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string ContactPerson { get; set; }
        /// <summary>
        /// 联系人职务
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string ContactPersonPost { get; set; }

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
