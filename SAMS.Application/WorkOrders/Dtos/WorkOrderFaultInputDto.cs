using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    
    public class WorkOrderFaultInputDto 
    {
      //  public virtual int? FaultEntryId { get; set; }
        public virtual int FaultId {get;set;}
        
        public virtual int? FaultReasonId { get; set; }
        public virtual int? FaultMeasureId { get; set; }
       // public ICollection<> Photos { get; set; }

    }
}