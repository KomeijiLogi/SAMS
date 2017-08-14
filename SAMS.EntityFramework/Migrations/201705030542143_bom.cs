namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.t_bd_bom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        AccessoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId, cascadeDelete: true)
                .ForeignKey("dbo.t_bd_product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.AccessoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.t_bd_bom", "ProductId", "dbo.t_bd_product");
            DropForeignKey("dbo.t_bd_bom", "AccessoryId", "dbo.t_bd_accessory");
            DropIndex("dbo.t_bd_bom", new[] { "AccessoryId" });
            DropIndex("dbo.t_bd_bom", new[] { "ProductId" });
            DropTable("dbo.t_bd_bom");
        }
    }
}
