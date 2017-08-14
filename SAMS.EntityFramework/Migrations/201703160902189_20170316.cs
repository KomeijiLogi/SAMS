namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _20170316 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_inv_StockBillEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessoryId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
               // .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId, cascadeDelete: true)
               // .ForeignKey("dbo.T_inv_StockBill", t => t.BillId, cascadeDelete: true)
                .Index(t => t.AccessoryId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.T_inv_StockBill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillType = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        StraffId = c.Long(),
                        Description = c.String(maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_StockBill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.StraffId)
                .Index(t => t.StraffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_inv_StockBill", "StraffId", "dbo.AbpUsers");
            DropForeignKey("dbo.T_inv_StockBillEntry", "BillId", "dbo.T_inv_StockBill");
            DropForeignKey("dbo.T_inv_StockBillEntry", "AccessoryId", "dbo.t_bd_accessory");
            DropIndex("dbo.T_inv_StockBill", new[] { "StraffId" });
            DropIndex("dbo.T_inv_StockBillEntry", new[] { "BillId" });
            DropIndex("dbo.T_inv_StockBillEntry", new[] { "AccessoryId" });
            DropTable("dbo.T_inv_StockBill",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_StockBill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_inv_StockBillEntry");
        }
    }
}
