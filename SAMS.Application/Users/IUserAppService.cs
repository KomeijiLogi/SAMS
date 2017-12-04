using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SAMS.Users.Dto;
using SAMS.Users.Dtos;
using SAMS.Authorization.Roles;

namespace SAMS.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);


        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);


        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);
        Task DeleteUser(long Id);
        ListResultDto<UserListDto> GetUsersByRole(Role role);
    }
}