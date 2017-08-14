using Abp.Application.Services.Dto;
using SAMS.Products.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.StaffMobile.Models
{
    public class CommitOrderViewModel
    {
        /// <summary>
        /// 产品信息
        /// </summary>
        public ListResultDto<ProductListDto> Products { get; set; }

        public SAMS.WorkOrders.Dtos.GetDetailOutput WorkOrder { get; set; }
    }
}