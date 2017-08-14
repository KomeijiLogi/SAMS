namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017011701 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_WO_WorkOrderBill", "AssignedPersonId", "dbo.AbpUsers");
            DropForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.T_BD_Customer");
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "CustomerId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "AssignedPersonId" });
            AlterColumn("dbo.T_WO_WorkOrderBill", "CustomerId", c => c.Int());
            AlterColumn("dbo.T_WO_WorkOrderBill", "AssignedPersonId", c => c.Long());
            CreateIndex("dbo.T_WO_WorkOrderBill", "CustomerId");
            CreateIndex("dbo.T_WO_WorkOrderBill", "AssignedPersonId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "AssignedPersonId", "dbo.AbpUsers", "Id");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.T_BD_Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.T_BD_Customer");
            DropForeignKey("dbo.T_WO_WorkOrderBill", "AssignedPersonId", "dbo.AbpUsers");
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "AssignedPersonId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "CustomerId" });
            AlterColumn("dbo.T_WO_WorkOrderBill", "AssignedPersonId", c => c.Long(nullable: false));
            AlterColumn("dbo.T_WO_WorkOrderBill", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderBill", "AssignedPersonId");
            CreateIndex("dbo.T_WO_WorkOrderBill", "CustomerId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.T_BD_Customer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_WO_WorkOrderBill", "AssignedPersonId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
    }
}
