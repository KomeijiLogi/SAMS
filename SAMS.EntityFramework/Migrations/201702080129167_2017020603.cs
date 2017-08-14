namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _2017020603 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boms",
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
                .PrimaryKey(t => t.Id)
               // .ForeignKey("dbo.T_BD_Material", t => t.ChildMaterialId, cascadeDelete: true)
               // .ForeignKey("dbo.T_BD_Material", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.ChildMaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boms", "MaterialId", "dbo.T_BD_Material");
            DropForeignKey("dbo.Boms", "ChildMaterialId", "dbo.T_BD_Material");
            DropIndex("dbo.Boms", new[] { "ChildMaterialId" });
            DropIndex("dbo.Boms", new[] { "MaterialId" });
            DropTable("dbo.Boms",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Bom_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
