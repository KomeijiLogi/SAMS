using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderAccessoryEntry))]
    public class WorkOrderAccessoryDto : EntityDto
    {
        public virtual int ParentId { get; set; }
        //public virtual string SerialNo { get; set; }
        //public virtual Direct Direct { get; set; }
        public virtual int AccessoryId { get; set; }
        public virtual string AccessoryName { get; set; }
        public virtual string AccessoryNumber { get; set; }
        public virtual string AccessoryModel { get; set; }
        public virtual string AccessoryUnit { get; set; }
        public virtual int Count { get; set; }
        public virtual int BackCount { get; set; }
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