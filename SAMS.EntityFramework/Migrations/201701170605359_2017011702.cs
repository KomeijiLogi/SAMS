namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017011702 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material");
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "EquipmentId" });
            AlterColumn("dbo.T_WO_WorkOrderBill", "EquipmentId", c => c.Int());
            CreateIndex("dbo.T_WO_WorkOrderBill", "EquipmentId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material");
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "EquipmentId" });
            AlterColumn("dbo.T_WO_WorkOrderBill", "EquipmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderBill", "EquipmentId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material", "Id", cascadeDelete: true);
        }
    }
}
