using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
   
    public class WorkOrderAccessoryInputDto 
    {
        public virtual string AccessoryId { get; set; }
        public virtual int Count { get; set; }
        /// <summary>
        /// 新件序号
        /// </summary>
        public virtual string NewSerials { get; set; }
        /// <summary>
        /// 旧件序号
        /// </summary>
        public virtual string OldSerials { get; set; }



    }
}