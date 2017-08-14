using Abp.Application.Services.Dto;
using SAMS.Faults.Dtos;
using SAMS.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Fault
{
    public class GetViewModel
    {
        public ListResultDto<FaultListDto> Faults { get; set; }
    }
}