using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.EquipmentArchives.Dto
{
    [AutoMapFrom(typeof(EquipmentArchive))]
    public class EquipmentArchiveDetailDto: EntityDto<int>
    {
        /// <summary>
        /// 产品
        /// </summary>
        public  string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public virtual DateTime? ServiceTime { get; set; }

        /// <summary>
        /// 最近服务日期
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public virtual DateTime? LastServiceTime { get; set; }
        /// <summary>
        /// 保修期至
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public virtual DateTime? Warrenty { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>
        public virtual String AssignedPersonName { get; set; }
        public virtual long? AssignedPersonId { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public virtual string CustomerName { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string CustomerId { get; set; }

        //装机分类
        public virtual string InstallType { get; set; }
    }
}
