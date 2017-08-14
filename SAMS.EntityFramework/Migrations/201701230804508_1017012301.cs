namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1017012301 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId");
           // AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure");
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultMeasureId" });
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId");
        }
    }
}
