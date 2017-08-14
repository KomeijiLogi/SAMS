namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201701120201 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason");
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultReasonId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultMeasureId" });
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultId", c => c.Int());
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", c => c.Int());
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", c => c.Int());
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultId");
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId");
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId");
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault", "Id");
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure", "Id");
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault");
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultMeasureId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultReasonId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultId" });
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", c => c.Int(nullable: true));
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", c => c.Int(nullable: true));
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId");
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId");
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultId");
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault", "Id", cascadeDelete: true);
        }
    }
}
