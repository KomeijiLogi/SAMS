using Abp.Application.Navigation;
using Abp.Localization;
using SAMS.Authorization;

namespace SAMS.Web.Areas.Admin.Startup
{
    public class AdminNavigationProvider : NavigationProvider
    {
        public const string MenuName = "Admin";
        
        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                //.AddItem(new MenuItemDefinition(
                //    "Dashboard",
                //    L("Dashboard"),
                //    url: "Admin/Dashboard",
                //    icon: "glyphicon glyphicon-home"

                //    //requiredPermissionName: AppPermissions.Pages_Tenants
                //    )
                //)
                .AddItem(new MenuItemDefinition("WorkOrder",
                    L("WorkOrder"),
                    url: "Admin/WorkOrder",
                    icon: "fa fa-file-text"

                    )
                   )
                .AddItem(new MenuItemDefinition("AccessoryStock",
                    L("AccessoryStock"),
                    url: "Admin/Accessory/NewOrgStock",
                    icon: "icon-puzzle bold"
                    )
                )
                .AddItem(new MenuItemDefinition("Customer",
                    L("Customer"),
                    url: "Admin/Customer",
                    icon: "glyphicon glyphicon-user"
                    ))
                .AddItem(new MenuItemDefinition("SystenSetting",
                    L("SystemSetting"),
                    url: "Admin/SystemSetting",
                    icon: "glyphicon glyphicon-cog"
                    ))
                ;

                //).AddItem(new MenuItemDefinition(
                //    PageNames.App.Host.Editions,
                //    L("Editions"),
                //    url: "Mpa/Editions",
                //    icon: "icon-grid",
                //    requiredPermissionName: AppPermissions.Pages_Editions
                //    )
                //).AddItem(new MenuItemDefinition(
                //    PageNames.App.Tenant.Dashboard,
                //    L("Dashboard"),
                //    url: "Mpa/Dashboard",
                //    icon: "icon-home",
                //    requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard
                //    )
                //).AddItem(new MenuItemDefinition(
                //    PageNames.App.Common.Administration,
                //    L("Administration"),
                //    icon: "icon-wrench"
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Common.OrganizationUnits,
                //        L("OrganizationUnits"),
                //        url: "Mpa/OrganizationUnits",
                //        icon: "icon-layers",
                //        requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Common.Roles,
                //        L("Roles"),
                //        url: "Mpa/Roles",
                //        icon: "icon-briefcase",
                //        requiredPermissionName: AppPermissions.Pages_Administration_Roles
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Common.Users,
                //        L("Users"),
                //        url: "Mpa/Users",
                //        icon: "icon-users",
                //        requiredPermissionName: AppPermissions.Pages_Administration_Users
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Common.Languages,
                //        L("Languages"),
                //        url: "Mpa/Languages",
                //        icon: "icon-flag",
                //        requiredPermissionName: AppPermissions.Pages_Administration_Languages
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Common.AuditLogs,
                //        L("AuditLogs"),
                //        url: "Mpa/AuditLogs",
                //        icon: "icon-lock",
                //        requiredPermissionName: AppPermissions.Pages_Administration_AuditLogs
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Host.Maintenance,
                //        L("Maintenance"),
                //        url: "Mpa/Maintenance",
                //        icon: "icon-wrench",
                //        requiredPermissionName: AppPermissions.Pages_Administration_Host_Maintenance
                //        )
                //    )
                //    .AddItem(new MenuItemDefinition(
                //        PageNames.App.Host.Settings,
                //        L("Settings"),
                //        url: "Mpa/HostSettings",
                //        icon: "icon-settings",
                //        requiredPermissionName: AppPermissions.Pages_Administration_Host_Settings
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //        PageNames.App.Tenant.Settings,
                //        L("Settings"),
                //        url: "Mpa/Settings",
                //        icon: "icon-settings",
                //        requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                //        )
                //    )
               // );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, "SAMS");
        }
    }
}