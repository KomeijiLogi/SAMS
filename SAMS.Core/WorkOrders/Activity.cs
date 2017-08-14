using Abp.Domain.Entities.Auditing;
using SAMS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
   [Table("T_WO_Activity")]
    public  class Activity: AuditedEntity
    {
        //处理人员
        [ForeignKey("OperaterId")]
        public virtual User Operater { get; set; }
        public virtual long? OperaterId { get; set; }

        [ForeignKey("BillId")]
        public virtual WorkOrderBill Bill { get; set; }
        public virtual int BillId { get; set; }
        //服务人员
        [ForeignKey("AssignedPersonId")]
        public virtual User AssignedPerson { get; set; }
        public virtual long? AssignedPersonId { get; set; }

        public virtual string Log { get; set; }
        public virtual string Name { get; set; }

    }
    ///// <summary>
    ///// 新建活动
    ///// </summary>
    //public class CreateActivity : Activity
    //{

    //}
    ///// <summary>
    ///// 新建活动
    ///// </summary>
    //public class EditActivity : Activity
    //{

    //}
    ///// <summary>
    ///// 受理活动
    ///// </summary>
    //public class AcceptActivity:Activity
    //{

    //}
    
    ///// <summary>
    ///// 派工活动
    ///// </summary>
    //public class DispatchActivity : Activity
    //{
    //    public virtual User AssignedPerson { get; set; }

    //}
    ///// <summary>
    ///// 再派工活动
    ///// </summary>
    //public class RedispatchActivity : Activity
    //{
    //    public virtual User AssignedPerson { get; set; }

    //}
    ///// <summary>
    ///// 回单活动
    ///// </summary>
    //public class ReportActivity : Activity
    //{

    //}
    ///// <summary>
    ///// 结算活动
    ///// </summary>
    //public class StatmentActivity : Activity
    //{

    //}
    ///// <summary>
    ///// 回访活动
    ///// </summary>
    //public class ReturnVisitActivity : Activity
    //{

    //}
    ///// <summary>
    ///// 关闭活动
    ///// </summary>
    //public class CloseActivity : Activity
    //{

    //}

}
