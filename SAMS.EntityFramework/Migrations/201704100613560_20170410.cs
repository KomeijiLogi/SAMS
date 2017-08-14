namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _20170410 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.t_bd_product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        EASNumber = c.String(),
                        K3Number = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Product_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.t_bd_product", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.t_bd_product", "DeleterUserId", c => c.Long());
            AddColumn("dbo.t_bd_product", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.t_bd_product", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.t_bd_product", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.t_bd_product", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.t_bd_product", "CreatorUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.t_bd_product", "CreatorUserId");
            DropColumn("dbo.t_bd_product", "CreationTime");
            DropColumn("dbo.t_bd_product", "LastModifierUserId");
            DropColumn("dbo.t_bd_product", "LastModificationTime");
            DropColumn("dbo.t_bd_product", "DeletionTime");
            DropColumn("dbo.t_bd_product", "DeleterUserId");
            DropColumn("dbo.t_bd_product", "IsDeleted");
            AlterTableAnnotations(
                "dbo.t_bd_product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        EASNumber = c.String(),
                        K3Number = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Product_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
