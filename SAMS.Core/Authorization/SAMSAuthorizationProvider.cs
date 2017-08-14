using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SAMS.Authorization
{
    public class SAMSAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            ////管理员权限
            //var admin = context.CreatePermission("Admin", L("Admin"));
            ////工程师权限
            //var staff = context.CreatePermission("Staff", L("Staff"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SAMSConsts.LocalizationSourceName);
        }
    }
}
