
using Abp.AutoMapper;
using System;

namespace SAMS.WorkOrders.Dtos
{
    
    public class CloseInput
    {
       
        
        /// <summary>
        /// 工单Id
        /// </summary>
        public virtual int BillId { get; set; }

    }
}