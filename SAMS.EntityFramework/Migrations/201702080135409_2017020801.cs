namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017020801 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Boms", newName: "T_BD_Bom");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.T_BD_Bom", newName: "Boms");
        }
    }
}
