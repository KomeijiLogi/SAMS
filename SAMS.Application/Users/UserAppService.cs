using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using SAMS.Authorization;
using SAMS.Users.Dto;
using Microsoft.AspNet.Identity;
using SAMS.Users.Dtos;
using System.Data.Entity;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using System.Linq;
using SAMS.Authorization.Roles;
using System.Collections.ObjectModel;
using Abp.Authorization.Users;
using Abp.Runtime.Session;
using Abp.UI;
using System;
using SAMS.Roles;
using SAMS.Roles.Dto;

namespace SAMS.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : SAMSAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly RoleManager _roleManager;
        IRepository<UserRole, long> _userRoleReposityory;
        public UserAppService(
            IRepository<User, long> userRepository, 
            IPermissionManager permissionManager,
             IRepository<UserRole, long> userRoleReposityory,
            RoleManager roleManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _roleManager = roleManager;
            _userRoleReposityory = userRoleReposityory;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }
        public  ListResultDto<UserListDto> GetUsersByRole(Role role)
        {
            var userIdList= _userRoleReposityory
                .GetAll()
                .Where(ur => ur.RoleId.Equals(role.Id))
                .Select(e=>e.UserId).ToArray();
            var users = _userRepository.GetAll().Where(e => userIdList.Contains(e.Id)).ToList();
            return new ListResultDto<UserListDto>(
               users.MapTo<List<UserListDto>>()
               );
        }

        public async Task<PagedResultDto<UserListDto>>GetUsers(GetUsersInput input)
        {
            var query = UserManager.Users
                .Include(u => u.Roles)

                .WhereIf(
                    !input.Name.IsNullOrWhiteSpace(),
                    u => u.Name.Contains(input.Name)
                    );
            var userCount = await query.CountAsync();
            var users = await query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            
            var userListDtos = users.MapTo<List<UserListDto>>();
            await FillRoleNames(userListDtos);
            return new PagedResultDto<UserListDto>(
                userCount,
                userListDtos
                );


        }
        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            var userRoleDtos = (await _roleManager.Roles.
                OrderBy(r => r.DisplayName)
                .Where(r=>r.Name.Equals("Staff"))
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync());
            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };
            if(!input.Id.HasValue)
            {
                output.User = new UserEditDto { IsActive = true };
                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);
                output.User = user.MapTo<UserEditDto>();
                foreach(var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(input.Id.Value, userRoleDto.RoleName);
                }
            }
            return output;

        }
        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue)
            {
                await UpdateUserAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }
        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            var user = await UserManager.FindByIdAsync(input.User.Id.Value);

            //Update user properties
            input.User.MapTo(user);

            if(!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }
            CheckErrors(await UserManager.UpdateAsync(user));

            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));
        }
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            var user = input.User.MapTo<User>();
            user.TenantId = AbpSession.TenantId;
            //Set password
            if(!input.User.Password.IsNullOrEmpty())
            {
                
            }
            else
            {
                input.User.Password = "789456";
            }
            user.Password = new PasswordHasher().HashPassword(input.User.Password);
            user.Roles = new Collection<UserRole>();
            foreach( var roleName in input.AssignedRoleNames)
            {
                
                try {
                    
                    string seekdata = "";
                   
                    if (roleName == "工程师") {
                        seekdata = "Staff";
                        
                    } else if (roleName == "Admin") {
                        seekdata = "Admin";
                      
                    }
                    else {

                    }
                    var role = await _roleManager.GetRoleByNameAsync(seekdata);
                    user.Roles.Add(new UserRole { RoleId = role.Id, TenantId = user.TenantId });


                } catch (Exception e) {


                };
                

                //var role = await _roleManager.GetRoleByNameAsync(roleName);
                //user.Roles.Add(new UserRole { RoleId = role.Id });
            }

            //UserManager.Create(user, user.Password);
            CheckErrors(await UserManager.CreateAsync(user));
            // await CurrentUnitOfWork.SaveChangesAsync();
            
           
        }
        public async Task DeleteUser(long Id)
        {
            if(Id==AbpSession.GetUserId())
            {
                throw new UserFriendlyException("不能删除自己");
            }
            var user = await UserManager.GetUserByIdAsync(Id);
            CheckErrors(await UserManager.DeleteAsync(user));
        }
        //public async Task CreateUser(CreateUserInput input)
        //{
        //    var user = input.MapTo<User>();

        //    user.TenantId = AbpSession.TenantId;
        //    user.Password = new PasswordHasher().HashPassword(input.Password);
        //    user.IsEmailConfirmed = true;

        //    CheckErrors(await UserManager.CreateAsync(user));
        //}
        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            var distinctRoleIds = (
                from userListDto in userListDtos
                from userLitRoleDto in userListDto.Roles
                select userLitRoleDto.RoleId
                ).Distinct();
            var roleNames = new Dictionary<int, string>();
            foreach(var roleId in distinctRoleIds)
            {
                roleNames[roleId] = (await _roleManager.GetRoleByIdAsync(roleId)).DisplayName;
            }
            foreach(var userListDto in userListDtos)
            {
                foreach(var userListRoleDto in userListDto.Roles)
                {
                    userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                }
                userListDto.Roles = userListDto.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }
    }
}