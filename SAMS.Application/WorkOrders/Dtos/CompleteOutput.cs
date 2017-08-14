
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    
    public class CompleteOutput
    {
        
        /// <summary>
        ///单据id 
        /// </summary>
        public virtual int BillId { get; set; }

        public virtual string Error { get; set; }
        public virtual bool Successed { get; set; }
        




    }
}