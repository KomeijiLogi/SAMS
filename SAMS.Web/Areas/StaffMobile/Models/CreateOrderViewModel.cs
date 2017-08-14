using Abp.Application.Services.Dto;
using SAMS.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.StaffMobile.Models
{
    public class CreateOrderViewModel
    {
        public ListResultDto<ProductListDto> Products { get; set; }

    }
}