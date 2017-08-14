using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using SAMS.Authorization.Roles;
using SAMS.Roles.Dto;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.Roles
{
    /* THIS IS JUST A SAMPLE. */
    public class RoleAppService : SAMSAppServiceBase,IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;

        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }

        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
        public  ListResultDto<RoleListDto>GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return new ListResultDto<RoleListDto>(roles.MapTo<List<RoleListDto>>());

        }
    }
}