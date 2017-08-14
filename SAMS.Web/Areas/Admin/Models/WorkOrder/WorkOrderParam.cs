using SAMS.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class WorkOrderParam
    {
        /// <summary>
        /// 工单Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
      
        public virtual ServiceType ServiceTypeID { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public virtual int? CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
     
        public virtual string CustomerName { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary> 
        public virtual string CustomerPhone { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
       
        public virtual string Area { get; set; }
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
        /// 服务描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
     
        public virtual int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>

        public virtual string ProductName { get; set; }
        /// <summary>
        /// 产品规格型号
        /// </summary>
        public virtual string ProductModel { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        
        public virtual PriorityEnum Priority { get; set; }


        public virtual string SerialNo { get; set; }
    }
}