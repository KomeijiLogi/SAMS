namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017020601 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_WO_WorkOrderMaterialEntry", name: "MeasureId", newName: "MaterialId");
            RenameIndex(table: "dbo.T_WO_WorkOrderMaterialEntry", name: "IX_MeasureId", newName: "IX_MaterialId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.T_WO_WorkOrderMaterialEntry", name: "IX_MaterialId", newName: "IX_MeasureId");
            RenameColumn(table: "dbo.T_WO_WorkOrderMaterialEntry", name: "MaterialId", newName: "MeasureId");
        }
    }
}
