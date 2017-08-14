using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using SAMS.Api;
using Hangfire;
using SAMS.Web.Areas.Admin.Startup;
using Abp.Web.Mvc.Authorization;
using SAMS.Web.Mvc.Authorization;
using System.Linq;
using SAMS.Web.Areas.StaffMobile.Startup;

namespace SAMS.Web
{
    [DependsOn(
        typeof(SAMSDataModule),
        typeof(SAMSApplicationModule),
        typeof(SAMSWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class SAMSWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<SAMSNavigationProvider>();
            Configuration.Navigation.Providers.Add<AdminNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

          

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AdminBundleConfig.RegisterBundles(BundleTable.Bundles);
            StaffMobileBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public override void PostInitialize()
        {
            base.PostInitialize();
           // GlobalFilters.Filters.Remove(GlobalFilters.Filters.Single(f => f.Instance is AbpMvcAuthorizeFilter));

          //  GlobalFilters.Filters.Add(IocManager.Resolve<AbpTicketMvcAuthorizeFilter>());
        }
    }
    
}
