namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iii : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.t_bd_Customer", "Code", c => c.String(maxLength: 500));
            AlterColumn("dbo.t_bd_Customer", "Name", c => c.String(maxLength: 500));
            AlterColumn("dbo.t_bd_Customer", "Address", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.t_bd_Customer", "Address", c => c.String(maxLength: 100));
            AlterColumn("dbo.t_bd_Customer", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.t_bd_Customer", "Code", c => c.String(maxLength: 50));
        }
    }
}
