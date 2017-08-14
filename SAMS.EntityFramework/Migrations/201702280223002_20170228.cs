namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170228 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_WO_WorkOrderBill", "Priority");
        }
    }
}
