using Abp.Application.Services.Dto;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.StaffMobile.Models
{
    public class DoneOrderViewModel
    {
        public PagedResultDto<WorkOrderListDto> WorkOrders { get; set; }

    }
}