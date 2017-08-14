using System.Linq;
using SAMS.EntityFramework;
using SAMS.MultiTenancy;

namespace SAMS.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly SAMSDbContext _context;

        public DefaultTenantCreator(SAMSDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
