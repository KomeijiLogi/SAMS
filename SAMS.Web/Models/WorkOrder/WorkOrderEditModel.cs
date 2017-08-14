using Abp.Application.Services.Dto;
using SAMS.Faults.Dtos;

using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Models.WorkOrder
{
    public class WorkOrderEditModel
    {
        public GetDetailOutput bill { get; set; }
        public ListResultDto<GetFaultDetailOutput> faults { get; set; }
        
    }
}