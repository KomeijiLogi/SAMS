namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170415 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "Mobile");
        }
    }
}
