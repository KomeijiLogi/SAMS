using Abp.Domain.Entities;
using SAMS.Customers;
using SAMS.Products;
using SAMS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.EquipmentArchives
{
    public class EquipmentArchive: Entity
    {
        /// <summary>
        /// 产品
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public virtual string ProductId { get; set; }

        /// <summary>
        /// 产品序列号
        /// </summary>
        public virtual string SerialNo { get; set; }
        /// <summary>
        /// 使用科室
        /// </summary>
        public virtual string Office { get; set; }
        /// <summary>
        /// 科室联系人
        /// </summary>
        public virtual string OfficePerson { get; set; }
        /// <summary>
        /// 科室联系人职务
        /// </summary>
        public virtual string OfficePosition { get; set; }
        /// <summary>
        /// 科室联系人电话
        /// </summary>
        public virtual string OfficeTel { get; set; }
        /// <summary>
        /// 科室联系人手机
        /// </summary>
        public virtual string OfficeMobile { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        public virtual DateTime? ServiceTime { get; set; }
        /// <summary>
        /// 保修期至
        /// </summary>
        public virtual DateTime? Warrenty { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>
        [ForeignKey("AssignedPersonId")]
        public virtual User AssignedPerson { get; set; }
        public virtual long? AssignedPersonId { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual string CustomerId { get; set; }

        //装机分类
        public virtual string InstallType { get; set; }
    }
}
