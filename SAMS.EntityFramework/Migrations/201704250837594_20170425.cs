namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170425 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "YUNMsg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_WO_WorkOrderBill", "YUNMsg");
        }
    }
}
