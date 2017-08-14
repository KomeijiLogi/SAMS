namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accessorySerialno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "NewSerials", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "OldSerials", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "OldSerials");
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "NewSerials");
        }
    }
}
