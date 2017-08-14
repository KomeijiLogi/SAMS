using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderBill))]
    public class GetWorkOrderListDto:EntityDto
    {
      

        /// <summary>
        /// 单号
        /// </summary>
        public virtual string Number { get; set; }
        
        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public virtual string EquipmentName { get;
            set;
        }

        /// <summary>
        /// 服务类型
        /// </summary>
        public virtual ServiceType ServiceType { get; set; }
        public virtual string ServiceTypeName() {
            switch(ServiceType)
            {
                case ServiceType.Repair:
                    return "维修";

                case ServiceType.Install:
                    return "安装";
            }
            return "";
        }
        /// <summary>
        /// 销售员
        /// </summary>
        public virtual string SaleMan { get; set; }
        /// <summary>
        /// 销售员电话
        /// </summary>
        public virtual string SaleManPhone { get; set; }
        /// <summary>
        /// 接单人姓名
        /// </summary>
        public virtual string AssignedPersonName { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public virtual DateTime? PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public virtual DateTime? PlanEndTime { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public virtual BillStatus BillStatus { get; set; }
        public virtual string BillStatusName()
        {
            switch ( BillStatus)
            {
               
               
                case BillStatus.Cancel:
                    return "已取消";
                case BillStatus.Close:
                    return "已关闭";


                case BillStatus.Dispatch:
                    return "已派工";
                case BillStatus.Save:
                    return "新建";
                case BillStatus.Accept:
                    return "已受理";
                case BillStatus.Complete:
                    return "已完工";
            }
            return "";
        }

        /// <summary>
        /// 下达时间
        /// </summary>
        public virtual DateTime? ReleaseTime { get; set; }


    }
}
