using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace SAMS.EquipmentArchives.Dto
{
    [AutoMapFrom(typeof(EquipmentArchive))]
    public class EquipmentArchiveListDto: EntityDto<int>
    {
        /// <summary>
        /// 产品序列号
        /// </summary>
        public  string SerialNo { get; set; }

        public  string ProductName { get; set; }
        public string ProductModel { get; set; }
        public  string CustomerName { get; set; }
        //装机分类
        public  string InstallType { get; set; }
        public  string AssignedPersonName { get; set; }
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
        /// 最后服务时间
        /// </summary>
        public virtual DateTime? LastServiceTime { get; set; }

    }
}