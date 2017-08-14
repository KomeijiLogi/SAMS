namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class report : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "Office", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "OfficePerson", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "OfficePosition", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "OfficeTel", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "OfficeMobile", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "ServiceTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderBill", "Warrenty", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderBill", "GuaranteedState", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_WO_WorkOrderBill", "GuaranteedState");
            DropColumn("dbo.T_WO_WorkOrderBill", "Warrenty");
            DropColumn("dbo.T_WO_WorkOrderBill", "ServiceTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "OfficeMobile");
            DropColumn("dbo.T_WO_WorkOrderBill", "OfficeTel");
            DropColumn("dbo.T_WO_WorkOrderBill", "OfficePosition");
            DropColumn("dbo.T_WO_WorkOrderBill", "OfficePerson");
            DropColumn("dbo.T_WO_WorkOrderBill", "Office");
        }
    }
}
