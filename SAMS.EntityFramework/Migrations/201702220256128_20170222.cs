namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _20170222 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_BD_Area", "ParentId", "dbo.T_BD_Area");
            DropForeignKey("dbo.T_BD_Customer", "CityId", "dbo.T_BD_Area");
            DropForeignKey("dbo.T_BD_CustomerLinkMan", "CustomerId", "dbo.T_BD_Customer");
            DropForeignKey("dbo.T_BD_Customer", "ProvinceId", "dbo.T_BD_Area");
            DropForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material");
            DropIndex("dbo.T_BD_CustomerLinkMan", new[] { "CustomerId" });
            DropIndex("dbo.T_BD_Customer", new[] { "ProvinceId" });
            DropIndex("dbo.T_BD_Customer", new[] { "CityId" });
            DropIndex("dbo.T_BD_Area", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "EquipmentId" });
            CreateTable(
                "dbo.t_bd_product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        EASNumber = c.String(),
                        K3Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.T_BD_Customer", "Area", c => c.String(maxLength: 100));
            AddColumn("dbo.T_BD_Customer", "ContactPerson", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_Customer", "ContactPersonPost", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_Customer", "Mobile", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_Customer", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.T_WO_WorkOrderBill", "ProductId", c => c.Int());
            CreateIndex("dbo.T_WO_WorkOrderBill", "ProductId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "ProductId", "dbo.t_bd_product", "Id");
            DropColumn("dbo.T_BD_Customer", "ProvinceId");
            DropColumn("dbo.T_BD_Customer", "CityId");
            DropColumn("dbo.T_WO_WorkOrderBill", "ContactPerson");
            DropColumn("dbo.T_WO_WorkOrderBill", "ContactPersonPost");
            DropColumn("dbo.T_WO_WorkOrderBill", "EquipmentId");
            DropTable("dbo.T_BD_CustomerLinkMan");
            DropTable("dbo.T_BD_Area",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Area_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.T_BD_Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        ParentId = c.Int(nullable: false),
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
                    { "DynamicFilter_Area_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_BD_CustomerLinkMan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ContactPerson = c.String(maxLength: 50),
                        ContactPersonPost = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.T_WO_WorkOrderBill", "EquipmentId", c => c.Int());
            AddColumn("dbo.T_WO_WorkOrderBill", "ContactPersonPost", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "ContactPerson", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_Customer", "CityId", c => c.Int());
            AddColumn("dbo.T_BD_Customer", "ProvinceId", c => c.Int());
            DropForeignKey("dbo.T_WO_WorkOrderBill", "ProductId", "dbo.t_bd_product");
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "ProductId" });
            DropColumn("dbo.T_WO_WorkOrderBill", "ProductId");
            DropColumn("dbo.T_BD_Customer", "Email");
            DropColumn("dbo.T_BD_Customer", "Mobile");
            DropColumn("dbo.T_BD_Customer", "ContactPersonPost");
            DropColumn("dbo.T_BD_Customer", "ContactPerson");
            DropColumn("dbo.T_BD_Customer", "Area");
            DropTable("dbo.t_bd_product");
            CreateIndex("dbo.T_WO_WorkOrderBill", "EquipmentId");
            CreateIndex("dbo.T_BD_Area", "ParentId");
            CreateIndex("dbo.T_BD_Customer", "CityId");
            CreateIndex("dbo.T_BD_Customer", "ProvinceId");
            CreateIndex("dbo.T_BD_CustomerLinkMan", "CustomerId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material", "Id");
            AddForeignKey("dbo.T_BD_Customer", "ProvinceId", "dbo.T_BD_Area", "Id");
            AddForeignKey("dbo.T_BD_CustomerLinkMan", "CustomerId", "dbo.T_BD_Customer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_BD_Customer", "CityId", "dbo.T_BD_Area", "Id");
            AddForeignKey("dbo.T_BD_Area", "ParentId", "dbo.T_BD_Area", "Id");
        }
    }
}
