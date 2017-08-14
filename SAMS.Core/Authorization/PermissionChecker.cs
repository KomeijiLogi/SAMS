using Abp.Authorization;
using SAMS.Authorization.Roles;
using SAMS.MultiTenancy;
using SAMS.Users;

namespace SAMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
