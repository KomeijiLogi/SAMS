namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _2107022201 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_BD_MaterialGroup", "ParentId", "dbo.T_BD_MaterialGroup");
            DropForeignKey("dbo.T_BD_Material", "AssignedGroupId", "dbo.T_BD_MaterialGroup");
            DropForeignKey("dbo.T_BD_Bom", "ChildMaterialId", "dbo.T_BD_Material");
            DropForeignKey("dbo.T_BD_Bom", "MaterialId", "dbo.T_BD_Material");
            DropIndex("dbo.T_BD_Bom", new[] { "MaterialId" });
            DropIndex("dbo.T_BD_Bom", new[] { "ChildMaterialId" });
            DropIndex("dbo.T_BD_Material", new[] { "AssignedGroupId" });
            DropIndex("dbo.T_BD_MaterialGroup", new[] { "ParentId" });
            CreateTable(
                "dbo.t_bd_accessory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        Unit = c.String(),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.T_BD_Bom",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Bom_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_Material",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Material_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_MaterialGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MaterialGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.T_BD_MaterialGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
                        ParentId = c.Int(),
                        Description = c.String(),
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
                    { "DynamicFilter_MaterialGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_BD_Material",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Number1 = c.String(),
                        Number2 = c.String(),
                        Name = c.String(),
                        Model = c.String(),
                        Unit = c.String(),
                        AssignedGroupId = c.Int(nullable: false),
                        Description = c.String(),
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
                    { "DynamicFilter_Material_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_BD_Bom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        ChildMaterialId = c.Int(nullable: false),
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
                    { "DynamicFilter_Bom_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.t_bd_accessory");
            CreateIndex("dbo.T_BD_MaterialGroup", "ParentId");
            CreateIndex("dbo.T_BD_Material", "AssignedGroupId");
            CreateIndex("dbo.T_BD_Bom", "ChildMaterialId");
            CreateIndex("dbo.T_BD_Bom", "MaterialId");
            AddForeignKey("dbo.T_BD_Bom", "MaterialId", "dbo.T_BD_Material", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_BD_Bom", "ChildMaterialId", "dbo.T_BD_Material", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_BD_Material", "AssignedGroupId", "dbo.T_BD_MaterialGroup", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_BD_MaterialGroup", "ParentId", "dbo.T_BD_MaterialGroup", "Id");
        }
    }
}
