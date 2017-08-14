using System.Threading.Tasks;
using Abp.Application.Services;
using SAMS.Roles.Dto;
using Abp.Application.Services.Dto;

namespace SAMS.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
        ListResultDto<RoleListDto> GetRoles();
    }
}
