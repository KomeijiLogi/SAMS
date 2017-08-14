using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SAMS.Users;
using Abp.AutoMapper;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderBill))]
    public class GetDetailOutput:EntityDto
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
        public string CustomerArea { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary> 
        public virtual string CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public virtual string CustomerLinkMan { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public virtual string SaleMan { get; set; }
        /// <summary>
        /// 销售员电话
        /// </summary>
        public virtual string SaleManPhone { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 设备规格
        /// </summary>
        public virtual string ProductModel { get; set; }
        /// <summary>
        /// 设备Id
        /// </summary>
        public virtual int ProductId { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public virtual string SerialNo { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public virtual ServiceType ServiceType { get; set; }

        /// <summary>
        /// 相关描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 接单人姓名
        /// </summary>
        public virtual string AssignedPersonName { get; set; }
        /// <summary>
        /// 接单人Id
        /// </summary>
        public virtual long? AssignedPersonId { get; set; }

        /// <summary>
        /// 派工时间
        /// </summary>
        public virtual DateTime? DispatchTime { get; set; }
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
        /// 服务日期
        /// </summary>
        public virtual DateTime? ServiceTime { get; set; }
        /// <summary>
        /// 保修期至
        /// </summary>
        public virtual DateTime? Warrenty { get; set; }
        /// <summary>
        /// 是否在保
        /// </summary>
        public virtual bool GuaranteedState { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public virtual BillStatus BillStatus { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public virtual PriorityEnum Priority { get; set; }
   

        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 故障分录
        /// </summary>
        public ICollection<WorkOrderFaultDto> FaultEntry { get; set; }

        /// <summary>
        /// 配件分录
        /// </summary>
        public ICollection<WorkOrderAccessoryDto> AccessoryEntry { get; set; }

        public ICollection<WorkOrderPhotoDto> Photos { get; set; }
        /// <summary>
        /// 活动日志
        /// </summary>
        public ICollection<ActivityDto> Activities { get; set; }
        
        /// <summary>
        /// 评价
        /// </summary>
        public virtual ServiceEvalutionDto ServiceEvalution { get; set; }


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
