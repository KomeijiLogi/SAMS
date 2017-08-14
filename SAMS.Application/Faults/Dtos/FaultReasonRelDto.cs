using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Faults.Dtos
{
    [AutoMapFrom(typeof(FaultReasonRel))]
    public class FaultReasonRelDto:EntityDto
    {
        public virtual int FaultId { get; set; }
        public virtual int FaultReasonId { get; set; }
        public virtual string FaultReasonName { get;set; }
        public virtual string FaultReasonNumber { get; set; }
    }
}
