namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateZD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "Faultap", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "Dealfa", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "TrafficUrban", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "TrafficLong", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "HotelEx", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "Supply", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "OtherEx", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_WO_WorkOrderBill", "OtherEx");
            DropColumn("dbo.T_WO_WorkOrderBill", "Supply");
            DropColumn("dbo.T_WO_WorkOrderBill", "HotelEx");
            DropColumn("dbo.T_WO_WorkOrderBill", "TrafficLong");
            DropColumn("dbo.T_WO_WorkOrderBill", "TrafficUrban");
            DropColumn("dbo.T_WO_WorkOrderBill", "Dealfa");
            DropColumn("dbo.T_WO_WorkOrderBill", "Faultap");
        }
    }
}
