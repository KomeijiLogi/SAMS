namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170118 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultId");
            AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault");
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultId" });
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultId");
        }
    }
}
