namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createbasedatatable201701143 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Areas", newName: "T_BD_Area");
            RenameTable(name: "dbo.FaultMeasureRels", newName: "T_BD_FaultMeasureRel");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.T_BD_FaultMeasureRel", newName: "FaultMeasureRels");
            RenameTable(name: "dbo.T_BD_Area", newName: "Areas");
        }
    }
}
