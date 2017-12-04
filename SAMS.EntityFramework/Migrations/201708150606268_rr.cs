namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficUrban", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficLong", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "HotelEx", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "Supply", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.T_WO_WorkOrderBill", "OtherEx", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_WO_WorkOrderBill", "OtherEx", c => c.String());
            AlterColumn("dbo.T_WO_WorkOrderBill", "Supply", c => c.String());
            AlterColumn("dbo.T_WO_WorkOrderBill", "HotelEx", c => c.String());
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficLong", c => c.String());
            AlterColumn("dbo.T_WO_WorkOrderBill", "TrafficUrban", c => c.String());
        }
    }
}
