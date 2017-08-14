using SAMS.EntityFramework;
using EntityFramework.DynamicFilters;

namespace SAMS.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly SAMSDbContext _context;

        public InitialHostDbBuilder(SAMSDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
