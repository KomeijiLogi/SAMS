using System.Data.Common;
using Abp.Zero.EntityFramework;
using SAMS.Authorization.Roles;
using SAMS.MultiTenancy;
using SAMS.Users;

using SAMS.Faults;
using SAMS.WorkOrders;
using SAMS.Customers;
using System.Data.Entity;
using SAMS.Products;
using SAMS.Accessories;
using SAMS.Inventory;
using SAMS.EquipmentArchives;

namespace SAMS.EntityFramework
{
    public class SAMSDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual IDbSet<Accessory> Accessories { get; set; }
        
        public virtual IDbSet<Fault> Faults { get; set; }
        public virtual IDbSet<ServiceEvalution> ServiceEvalution { get; set; }
        public virtual IDbSet<StockBill> StockBills { get; set; }
        public virtual IDbSet<StockBillEntry> StockBillEntries { get; set; }
        public virtual IDbSet<StaffStock> StaffStocks { get; set; }
        public virtual IDbSet<OrgStock> OrgStocks { get; set; }
        public virtual IDbSet<FaultGroup> FaultGroups { get; set; }
        public virtual IDbSet<FaultReason> FaultReasons { get; set; }
        public virtual IDbSet<FaultMeasure> FaultMeasures { get; set; }
        public virtual IDbSet<FaultMeasureRel> FaultMeasureRels { get; set; }
        public virtual IDbSet<FaultReasonRel> FaultReasonRels { get; set; }
        public virtual IDbSet<WorkOrderBill> WorkOrderBills { get; set; }
        public virtual IDbSet<WorkOrderFaultEntry> WorkOrderFaultEntries { get; set; }
        public virtual IDbSet<WorkOrderPhoto> WorkOrderPhotos { get; set; }
        public virtual IDbSet<WorkOrderAccessoryEntry> WorkOrderAccessoryEntries { get; set; }
       
        public virtual IDbSet<Customer> Customers { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<Bom> Bom { get; set; }
        public virtual IDbSet<Activity> Activities { get; set; }
        public virtual IDbSet<EquipmentArchive> EquipmentArchives { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SAMSDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in SAMSDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SAMSDbContext since ABP automatically handles it.
         */
        public SAMSDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SAMSDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<WorkOrderFaultEntry>()
            //    .HasRequired<WorkOrderBill>(t => t.Bill)
            //    .WithMany(t => t.FaultEntry)
            //    .HasForeignKey(t => t.ParentId)
            //    .WillCascadeOnDelete(true);
        }
    }
}
