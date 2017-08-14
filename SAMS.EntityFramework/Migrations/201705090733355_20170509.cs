namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170509 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderPhoto", "FileType", c => c.String());
            DropColumn("dbo.T_WO_WorkOrderBill", "CustomerPhone");
            DropColumn("dbo.T_WO_WorkOrderBill", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "Email", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "CustomerPhone", c => c.String(maxLength: 50));
            DropColumn("dbo.T_WO_WorkOrderPhoto", "FileType");
        }
    }
}
