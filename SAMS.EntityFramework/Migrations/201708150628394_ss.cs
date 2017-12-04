namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficUrban", c => c.Decimal(nullable: true, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficLong", c => c.Decimal(nullable: true, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "HotelEx", c => c.Decimal(nullable: true, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "Supply", c => c.Decimal(nullable: true, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "OtherEx", c => c.Decimal(nullable: true, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_WO_WorkOrderBill", "OtherEx", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "Supply", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "HotelEx", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficLong", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficUrban", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
