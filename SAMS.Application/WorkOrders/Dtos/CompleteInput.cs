
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

        
    }
}