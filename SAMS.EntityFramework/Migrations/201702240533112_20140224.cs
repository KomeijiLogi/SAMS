namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20140224 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.T_BD_Customer", "ContactPerson");
            DropColumn("dbo.T_BD_Customer", "ContactPersonPost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_BD_Customer", "ContactPersonPost", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_Customer", "ContactPerson", c => c.String(maxLength: 50));
        }
    }
}
