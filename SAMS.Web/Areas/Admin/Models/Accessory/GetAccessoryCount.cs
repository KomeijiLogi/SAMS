using Abp.AutoMapper;
using SAMS.Accessories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Accessory
{
    [AutoMapFrom(typeof(GetAccessoryDto))]
    public class GetAccessoryCount: GetAccessoryDto
    {
        public int Count { get; set; }
    }
}