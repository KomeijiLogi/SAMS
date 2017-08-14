using System.Collections.Generic;
using Abp.Application.Services.Dto;
using WEGO.WOne.Editions.Dto;

namespace WEGO.WOne.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}