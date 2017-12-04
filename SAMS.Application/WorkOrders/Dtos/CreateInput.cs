
using Abp.AutoMapper;
using System;

namespace SAMS.WorkOrders.Dtos
{
    
    public class CreateInput
    {
       
        /// <summary>
        /// 单号
        /// </summary>
        public virtual string Number { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public virtual int CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary> 
        public virtual string CustomerPhone { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public virtual int ProvinceId { get; set; }
        public virtual int CityId { get; set; }

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
        /// 销售员
        /// </summary>
        public virtual string SaleMan { get; set; }
        /// <summary>
        /// 销售员电话
        /// </summary>
        public virtual string SaleManPhone { get; set; }
    
        /// <summary>
        /// 设备Id
        /// </summary>
        public virtual int? EquipmentId { get; set; }

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

        //装机分类
        public string InstallType { get; set; }
        



    }
}