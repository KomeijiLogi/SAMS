using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
   
    public class WorkOrderAccessoryInputDto 
    {
        public virtual int AccessoryId { get; set; }
        public virtual int Count { get; set; }
       
    

    }
}