
using Abp.AutoMapper;
using System;

namespace SAMS.WorkOrders.Dtos
{
    
    public class ExpenseInput
    {
        /// <summary>
        ///单据id 
        /// </summary>
        public virtual int BillId { get; set; }
        //市内交通
        public virtual decimal TrafficUrban { get; set; }

        //长途交通
        public virtual decimal TrafficLong { get; set; }

        //住宿费
        public virtual decimal HotelEx { get; set; }

        //补助
        public virtual decimal Supply { get; set; }
        //其他费用
        public virtual decimal OtherEx { get; set; }




    }
}