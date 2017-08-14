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
    public class FaultListDto: EntityDto
    {
        public string Name { get; set; }

      
        
    }
}
