namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10170123033 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "MeasureId", c => c.Int(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderMaterialEntry", "MeasureId");
            //AddForeignKey("dbo.T_WO_WorkOrderMaterialEntry", "MeasureId", "dbo.T_BD_Material", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderMaterialEntry", "MeasureId", "dbo.T_BD_Material");
            DropIndex("dbo.T_WO_WorkOrderMaterialEntry", new[] { "MeasureId" });
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "MeasureId");
        }
    }
}
