using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Faults.Dtos
{
    [AutoMapFrom(typeof(FaultMeasureRel))]
    public class FaultMeasureRelDto:EntityDto
    {
        public virtual int FaultId { get; set; }
        public virtual int FaultMeasureId { get; set; }
        public virtual string FaultMeasureName { get; set; }
        public virtual string FaultMeasureNumber { get; set; }
    }
}
