namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170206 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "NewSerialNo", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "OldSerialNo", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "Qty", c => c.Int(nullable: false));
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "SerialNo");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "Direct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "Direct", c => c.Byte(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "SerialNo", c => c.String(maxLength: 100));
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "Qty");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "OldSerialNo");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "NewSerialNo");
        }
    }
}
