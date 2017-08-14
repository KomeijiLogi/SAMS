using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using SAMS.EntityFramework;

namespace SAMS.Migrator
{
    [DependsOn(typeof(SAMSDataModule))]
    public class SAMSMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<SAMSDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}