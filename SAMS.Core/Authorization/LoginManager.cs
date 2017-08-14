using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using SAMS.Authorization.Roles;
using SAMS.MultiTenancy;
using SAMS.Users;
using System;
using System.Threading.Tasks;
using YUN;

namespace SAMS.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        public LogInManager(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            RoleManager roleManager)
            : base(
                  userManager,
                  multiTenancyConfig,
                  tenantRepository,
                  unitOfWorkManager,
                  settingManager,
                  userLoginAttemptRepository,
                  userManagementConfig,
                  iocResolver,
                  roleManager)
        {
        }
        /// <summary>
        /// 云之家token登录
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
        public virtual async Task <AbpLoginResult<Tenant,User>> LoginAsync(string ticket, string appID, string appSecret , string yunXT ,string tenancyName=null)
        {
            var result = await LoginAsyncInternal(ticket, appID, appSecret, yunXT,tenancyName);
            await SaveLoginAttempt(result, tenancyName, ticket);
            return result;
        }
        protected virtual async Task<AbpLoginResult<Tenant,User>> LoginAsyncInternal(string yunToken,string appID, string appSecret , string yunXT,string teancyName)
        {
            if(string.IsNullOrWhiteSpace(yunToken))
            {
                throw new ArgumentException("login");
            }
            Tenant tenant = null;
            if(!MultiTenancyConfig.IsEnabled)
            {
                tenant = await GetDefaultTenantAsync();
            }
            else if(!string.IsNullOrWhiteSpace(teancyName))
            {
                tenant = await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == teancyName);
                if(tenant==null)
                {
                    return new AbpLoginResult<Tenant, User>(AbpLoginResultType.TenantIsNotActive, tenant);
                }
            }
            int? tenantId = tenant == null ? (int?)null : tenant.Id;
            using (UnitOfWorkManager.Current.SetTenantId(tenantId))
            {
                //解析云之家token
                var yunUser = await GetUserByToken(yunToken,  appID,  appSecret, yunXT);
                string userName = yunUser.mobile;
                var user = await UserManager.AbpStore.FindByNameAsync(userName);
                if(user==null)
                {
                    return new AbpLoginResult<Tenant, User>(AbpLoginResultType.InvalidUserNameOrEmailAddress);
                }
                return await CreateLoginResultAsync(user, tenant);

            }

        }
        protected async Task<YunUser> GetUserByToken(string ticket, string appID, string appSecret, string yunXT)
        {
           YUNAPI.YUNXT = yunXT;
           YunUser user = await YUNAPI.GetYunUser(ticket, appID, appSecret);
           return user;
        }
    }
}
