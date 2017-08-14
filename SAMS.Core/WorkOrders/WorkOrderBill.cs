using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using SAMS.Faults;
using SAMS.Users;
using SAMS.Customers;
using System.ComponentModel.DataAnnotations;
using SAMS.Accessories;
using SAMS.Products;

namespace SAMS.WorkOrders
{
    [Table("T_WO_WorkOrderBill")]
    public class WorkOrderBill:FullAuditedEntity
    {
        public const int MaxNumberLength = 50;
        public const int MaxNameLength = 50;
        public const int MaxDescriptionLength = 2048;
        public const int MaxAddressLength = 100;
        public const int MaxPhoneLength = 50;
        public const int MaxSerialNoLength = 100;
        /// <summary>
        /// 单号
        /// </summary>
        [StringLength(MaxNumberLength)]
        public virtual string Number { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        [StringLength(MaxAddressLength)]
        public virtual String Address { get; set; }


        /// <summary>
        /// 客户电话
        /// </summary>
        //[StringLength(MaxPhoneLength)]
       // public virtual string CustomerPhone { get; set; }

   
        
        /// <summary>
        /// 客户联系人邮箱
        /// </summary>
       // public virtual string Email { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string SaleMan { get; set; }
        /// <summary>
        /// 销售员电话
        /// </summary>
        [StringLength(MaxPhoneLength)]
        public virtual string SaleManPhone { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public virtual int? ProductId { get; set; }

        /// <summary>
        /// 产品序列号
        /// </summary>
        [StringLength(MaxSerialNoLength)]
        public virtual string SerialNo { get; set; }

      
        /// <summary>
        /// 服务类型
        /// </summary>
        public virtual ServiceType ServiceType { get; set; }

        /// <summary>
        /// 服务描述
        /// </summary>
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>
        [ForeignKey("AssignedPersonId")]
        public virtual User AssignedPerson { get; set; }
        public  virtual long? AssignedPersonId { get; set; }

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
        public virtual bool  GuaranteedState { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public virtual BillStatus BillStatus { get;  set; }

        public virtual  PriorityEnum Priority { get; set; }
        /// 故障分录
        /// </summary>
        public virtual ICollection<WorkOrderFaultEntry> FaultEntry { get; set; }
        /// <summary>
        /// 配件分录
        /// </summary>
        public virtual ICollection<WorkOrderAccessoryEntry> AccessoryEntry { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public virtual ICollection<WorkOrderPhoto> Photos { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        public virtual ICollection<Activity> Activities { get; set; }
        public WorkOrderBill()
        {
           this.BillStatus = BillStatus.Save; 
        }
        [ForeignKey("ServiceEvalutionId")]
        public virtual ServiceEvalution ServiceEvalution { get; set; }
        public virtual int? ServiceEvalutionId { get; set; }

        public virtual string YUNMsg { get; set; }

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
