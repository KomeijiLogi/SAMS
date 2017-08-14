
using Abp.AutoMapper;
using System;

namespace SAMS.WorkOrders.Dtos
{
    
    public class Reportnput
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public virtual int BillId { get; set; }
     
        /// <summary>
        /// 客户联系人
        /// </summary>
        public virtual string CustomerLinkMan { get; set; }
        /// <summary>
        /// 客户联系人职务
        /// </summary>
        public virtual string CustomerLinkManPost { get; set; }
        /// <summary>
        /// 客户邮箱
        /// </summary>
        public virtual string CustomerEmail { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public virtual string CustomerPhone { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public virtual string SerialNo { get; set; }

        /// <summary>
        /// 设备安装位置
        /// </summary>
        public virtual string EquLocation { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public virtual ServiceType ServiceType { get; set; }

        /// <summary>
        /// 相关描述
        /// </summary>
        public virtual string Description { get; set; }

        
        /// <summary>
        /// 接单人Id
        /// </summary>
        public virtual long? AssignedPersonId { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public virtual DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public virtual DateTime PlanEndTime { get; set; }





    }
}