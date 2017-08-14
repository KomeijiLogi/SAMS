using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderFaultEntry))]
    public class WorkOrderFaultDto : EntityDto
    {
        public virtual int ParentId { get; set; }
        public virtual FaultDto Fault {get;set;}
        public virtual string FaultReasonName { get; set; }
        public virtual string FaultMeasureName { get; set; }
        public virtual int FaultReasonId { get; set; }
        public ICollection<FaultPhotoDto> Photos { get; set; }

    }
}