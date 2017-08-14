using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SAMS.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(Fault))]
    public class FaultDto:EntityDto
    {
        public virtual string Number { get; set; }
        public virtual string Name { get; set; }
        public virtual int? AssignedGroupId { get; set; }
        public virtual string AssignedGroupName { get; set; }

    }
}
