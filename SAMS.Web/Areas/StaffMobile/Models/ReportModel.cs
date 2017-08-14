using Abp.Application.Services.Dto;
using SAMS.Faults.Dtos;
using SAMS.Inventory.Dto;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.StaffMobile.Models.WorkOrder
{
    public class ReportModel
    {
        public GetDetailOutput WorkOrder { get; set; }
        public ListResultDto<StaffStockDto> MyAccessories { get; set; }
        public ListResultDto<FaultListDto> Faults { get; set; }
    }
}