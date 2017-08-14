namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170221 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.T_WO_WorkOrderBill", "EquLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "EquLocation", c => c.String(maxLength: 100));
        }
    }
}
