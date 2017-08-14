namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170309 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.t_inv_StaffStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AccessoryId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
               // .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId, cascadeDelete: true)
                //.ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AccessoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.t_inv_StaffStock", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.t_inv_StaffStock", "AccessoryId", "dbo.t_bd_accessory");
            DropIndex("dbo.t_inv_StaffStock", new[] { "AccessoryId" });
            DropIndex("dbo.t_inv_StaffStock", new[] { "UserId" });
            DropTable("dbo.t_inv_StaffStock");
        }
    }
}
