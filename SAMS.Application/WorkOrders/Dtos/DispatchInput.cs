
using Abp.AutoMapper;
using System;

namespace SAMS.WorkOrders.Dtos
{
    
    public class DispatchInput
    {
       
        /// <summary>
        /// 工单Id
        /// </summary>
        public virtual int BillId { get; set; }
        /// <summary>
        /// 接单人Id
        /// </summary>
        public virtual long UserId { get; set; }



    }
}