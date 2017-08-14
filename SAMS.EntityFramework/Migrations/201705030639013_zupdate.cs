namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.t_bd_bom", "AccessoryId", "dbo.t_bd_accessory");
            DropForeignKey("dbo.t_bd_bom", "ProductId", "dbo.t_bd_product");
            DropIndex("dbo.t_bd_bom", new[] { "ProductId" });
            DropIndex("dbo.t_bd_bom", new[] { "AccessoryId" });
            DropTable("dbo.t_bd_bom");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.t_bd_bom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        AccessoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.t_bd_bom", "AccessoryId");
            CreateIndex("dbo.t_bd_bom", "ProductId");
            AddForeignKey("dbo.t_bd_bom", "ProductId", "dbo.t_bd_product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.t_bd_bom", "AccessoryId", "dbo.t_bd_accessory", "Id", cascadeDelete: true);

        }
    }
}
