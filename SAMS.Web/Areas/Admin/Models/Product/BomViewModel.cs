using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAMS.Products.Dtos;
using Abp.Application.Services.Dto;
namespace SAMS.Web.Areas.Admin.Models.Product
{
    public class BomViewModel
    {
        public ListResultDto<BomDto> Boms { get; set; }
        public GetDetailOutput Product { get; set; }
       
    }
}