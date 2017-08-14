using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Faults.Dtos
{
    [AutoMapFrom(typeof(Fault))]
    public class GetFaultDetailOutput: EntityDto
    {
       
        public virtual string Number { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<FaultMeasureRelDto> FaultMeasureRels { get; set; }
    
        public virtual ICollection<FaultReasonRelDto> FaultReasonRels { get; set; }
    }
}
