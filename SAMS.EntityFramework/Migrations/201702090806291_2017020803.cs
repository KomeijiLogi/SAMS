namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _2017020803 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderFaultEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        FaultId = c.Int(nullable: false),
                        FaultReasonId = c.Int(nullable: false),
                        FaultMeasureId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderFaultEntry_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderFaultEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
                        ParentId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderFaultEntryPhoto_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderMaterialEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        NewSerialNo = c.String(maxLength: 100),
                        OldSerialNo = c.String(maxLength: 100),
                        MaterialId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderMaterialEntry_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderMaterialEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
                        ParentId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderMaterialEntryPhoto_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "IsDeleted");
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "DeleterUserId");
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "DeletionTime");
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "LastModificationTime");
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "LastModifierUserId");
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "CreationTime");
            DropColumn("dbo.T_WO_WorkOrderFaultEntry", "CreatorUserId");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "IsDeleted");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "DeleterUserId");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "DeletionTime");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "LastModificationTime");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "LastModifierUserId");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "CreationTime");
            DropColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "CreatorUserId");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "IsDeleted");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "DeleterUserId");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "DeletionTime");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "LastModificationTime");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "LastModifierUserId");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "CreationTime");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "CreatorUserId");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "IsDeleted");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "DeleterUserId");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "DeletionTime");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "LastModificationTime");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "LastModifierUserId");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "CreationTime");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "CreatorUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "DeleterUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntryPhoto", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "CreatorUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "DeleterUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "CreatorUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "DeleterUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderFaultEntryPhoto", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "CreatorUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "DeleterUserId", c => c.Long());
            AddColumn("dbo.T_WO_WorkOrderFaultEntry", "IsDeleted", c => c.Boolean(nullable: false));
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderMaterialEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
                        ParentId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderMaterialEntryPhoto_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderMaterialEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        NewSerialNo = c.String(maxLength: 100),
                        OldSerialNo = c.String(maxLength: 100),
                        MaterialId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderMaterialEntry_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderFaultEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
                        ParentId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderFaultEntryPhoto_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.T_WO_WorkOrderFaultEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        FaultId = c.Int(nullable: false),
                        FaultReasonId = c.Int(nullable: false),
                        FaultMeasureId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_WorkOrderFaultEntry_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
