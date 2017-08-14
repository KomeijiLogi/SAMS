using Abp.Application.Services.Dto;
using SAMS.Inventory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.StaffMobile.Models
{
    public class AccessoryViewModel
    {
        public ListResultDto<StaffStockDto> MyAccessories { get; set; }
    }
}