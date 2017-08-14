using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using SAMS.Migrations.SeedData;
using EntityFramework.DynamicFilters;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Migrations.Model;

namespace SAMS.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<SAMS.EntityFramework.SAMSDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            
            AutomaticMigrationsEnabled = false;
            ContextKey = "SAMS";
            SetSqlGenerator("system.Data.SqlClient", new ExtendedSqlGenerator());
        }

        protected override void Seed(SAMS.EntityFramework.SAMSDbContext context)
        {

            
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
    public class ExtendedSqlGenerator:SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddForeignKeyOperation op)
        {
            return;
        }
        protected override void Generate(DropForeignKeyOperation op)
        {
            return;
        }
    }
}
