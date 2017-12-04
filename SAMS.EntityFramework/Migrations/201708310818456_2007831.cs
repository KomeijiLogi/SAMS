namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2007831 : DbMigration
    {
        public override void Up()
        {
           // AlterColumn("dbo.T_WO_WorkOrderBill", "GuaranteedState", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.T_WO_WorkOrderBill", "GuaranteedState", c => c.Boolean());
        }
    }
}
