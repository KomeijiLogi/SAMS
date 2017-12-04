using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMap(typeof(WorkOrderBill))]
    public class CreateOrUpdateWorkOrderInput
    {
       
        //public WorkOrderEditDto WorkOrder { get; set; }
        /// <summary>
        /// 工单Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        [Required]
        public ServiceType ServiceType { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary> 
        public string CustomerPhone { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>

        public string CustomerArea { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>

        public string SaleMan { get; set; }
        /// <summary>
        /// 销售员电话
        /// </summary>
        public string SaleManPhone { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        public string IssueBill { get; set; }
        /// <summary>
        /// 销售公司
        /// </summary>
        public string SaleOrg { get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [Required]
        public string ProductId { get; set; }
        ///// <summary>
        ///// 产品名称
        ///// </summary>

        //public string ProductName { get; set; }
        ///// <summary>
        ///// 产品规格型号
        ///// </summary>
        //public string ProductModel { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Required]
        public PriorityEnum Priority { get; set; }
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
        //装机分类
        public string InstallType { get; set; }

    }
}