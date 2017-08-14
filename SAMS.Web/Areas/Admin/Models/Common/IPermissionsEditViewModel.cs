using System.Collections.Generic;
using WEGO.WOne.Authorization.Dto;

namespace WEGO.WOne.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}