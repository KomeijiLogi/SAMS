namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10170123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId");
           // AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason");
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultReasonId" });
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId");
        }
    }
}
