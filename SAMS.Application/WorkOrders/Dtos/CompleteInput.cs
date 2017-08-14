
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    
    public class CompleteInput
    {
       /// <summary>
       ///单据id 
       /// </summary>
        public virtual int BillId { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public virtual string SerialNo { get; set; }

        /// <summary>
        /// 故障分录
        /// </summary>
        public IEnumerable<WorkOrderFaultInputDto> Faults { get; set; }

        ///// <summary>
        ///// 配件分录
        ///// </summary>
        public IEnumerable<WorkOrderAccessoryInputDto> Accessories { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public virtual IEnumerable<WorkOrderPhotoDto> Photos { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary> 
        //public virtual string CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        //public virtual string CustomerLinkMan { get; set; }
        /// <summary>
        /// 客户联系人职务
        /// </summary>
        //public virtual string ContactPersonPost { get; set; }

        /// <summary>
        /// 客户联系人邮箱
        /// </summary>
        //public virtual string Email { get; set; }


        ///// <summary>
        ///// 设备安装位置
        ///// </summary>
        //public virtual string EquLocation { get; set; }

        ///// <summary>
        ///// 计划开始时间
        ///// </summary>
        //public virtual DateTime StartTime { get; set; }

        ///// <summary>
        ///// 计划结束时间
        ///// </summary>
        //public virtual DateTime EndTime { get; set; }
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
        /// 保修期至
        /// </summary>
        public virtual DateTime? Warrenty { get; set; }
        /// <summary>
        /// 是否在保
        /// </summary>
        public virtual bool GuaranteedState { get; set; }
        /// <summary>
        /// 服务日期
        /// </summary>
        public virtual DateTime? ServiceTime { get; set; }


        //故障现象
        public virtual string Faultap { get; set; }

        //处理方案
        public virtual string Dealfa { get; set; }

        //市内交通
        public virtual string TrafficUrban { get; set; }

        //长途交通
        public virtual string TrafficLong { get; set; }

        //住宿费
        public virtual string HotelEx { get; set; }

        //补助
        public virtual string Supply { get; set; }
        //其他费用
        public virtual string OtherEx { get; set; }

    }
}