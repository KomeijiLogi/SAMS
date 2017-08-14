namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017031601 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.t_inv_OrgStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessoryId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId, cascadeDelete: true)
                .Index(t => t.AccessoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.t_inv_OrgStock", "AccessoryId", "dbo.t_bd_accessory");
            DropIndex("dbo.t_inv_OrgStock", new[] { "AccessoryId" });
            DropTable("dbo.t_inv_OrgStock");
        }
    }
}
