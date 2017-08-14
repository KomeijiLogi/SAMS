using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using SAMS.EntityFramework;
using SAMS.Migrations;

namespace SAMS
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(SAMSCoreModule))]
    public class SAMSDataModule : AbpModule
    {
        public override void PreInitialize()
        {
           
            Database.SetInitializer(new CreateDatabaseIfNotExists<SAMSDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SAMSDbContext, Configuration>());
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
