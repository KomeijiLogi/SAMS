namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class createbasedatatable20170114 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_BD_FaultGroup",
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
                    { "DynamicFilter_FaultGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_BD_FaultGroup", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.FaultMeasureRels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaultId = c.Int(nullable: false),
                        FaultMeasureId = c.Int(nullable: false),
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
                    { "DynamicFilter_FaultMeasureRel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_BD_Fault", t => t.FaultId, cascadeDelete: true)
                .ForeignKey("dbo.T_BD_FaultMeasure", t => t.FaultMeasureId, cascadeDelete: true)
                .Index(t => t.FaultId)
                .Index(t => t.FaultMeasureId);
            
            CreateTable(
                "dbo.T_BD_Fault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
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
                    { "DynamicFilter_Fault_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_BD_FaultGroup", t => t.AssignedGroupId, cascadeDelete: true)
                .Index(t => t.AssignedGroupId);
            
            CreateTable(
                "dbo.T_BD_FaultReasonRel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaultId = c.Int(nullable: false),
                        FaultReasonId = c.Int(nullable: false),
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
                    { "DynamicFilter_FaultReasonRel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_BD_Fault", t => t.FaultId, cascadeDelete: true)
                .ForeignKey("dbo.T_BD_FaultReason", t => t.FaultReasonId, cascadeDelete: true)
                .Index(t => t.FaultId)
                .Index(t => t.FaultReasonId);
            
            CreateTable(
                "dbo.T_BD_FaultReason",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
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
                    { "DynamicFilter_FaultReason_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_BD_FaultMeasure",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
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
                    { "DynamicFilter_FaultMeasure_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_BD_MaterialGroup", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.T_BD_Material",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Number1 = c.String(),
                        Number2 = c.String(),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_BD_MaterialGroup", t => t.AssignedGroupId, cascadeDelete: true)
                .Index(t => t.AssignedGroupId);
            
            CreateTable(
                "dbo.T_WO_WorkOrderBill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        UserId = c.Long(nullable: false),
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
                    { "DynamicFilter_WorkOrderBill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_WO_WorkOrderFaultEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                    { "DynamicFilter_WorkOrderFaultEntry_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_WO_WorkOrderBill", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.T_WO_WorkOrderMaterialEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                    { "DynamicFilter_WorkOrderMaterialEntry_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_WO_WorkOrderBill", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderMaterialEntry", "ParentId", "dbo.T_WO_WorkOrderBill");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "ParentId", "dbo.T_WO_WorkOrderBill");
            DropForeignKey("dbo.T_WO_WorkOrderBill", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.T_BD_Material", "AssignedGroupId", "dbo.T_BD_MaterialGroup");
            DropForeignKey("dbo.T_BD_MaterialGroup", "ParentId", "dbo.T_BD_MaterialGroup");
            DropForeignKey("dbo.FaultMeasureRels", "FaultMeasureId", "dbo.T_BD_FaultMeasure");
            DropForeignKey("dbo.T_BD_FaultReasonRel", "FaultReasonId", "dbo.T_BD_FaultReason");
            DropForeignKey("dbo.T_BD_FaultReasonRel", "FaultId", "dbo.T_BD_Fault");
            DropForeignKey("dbo.FaultMeasureRels", "FaultId", "dbo.T_BD_Fault");
            DropForeignKey("dbo.T_BD_Fault", "AssignedGroupId", "dbo.T_BD_FaultGroup");
            DropForeignKey("dbo.T_BD_FaultGroup", "ParentId", "dbo.T_BD_FaultGroup");
            DropIndex("dbo.T_WO_WorkOrderMaterialEntry", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "UserId" });
            DropIndex("dbo.T_BD_Material", new[] { "AssignedGroupId" });
            DropIndex("dbo.T_BD_MaterialGroup", new[] { "ParentId" });
            DropIndex("dbo.T_BD_FaultReasonRel", new[] { "FaultReasonId" });
            DropIndex("dbo.T_BD_FaultReasonRel", new[] { "FaultId" });
            DropIndex("dbo.T_BD_Fault", new[] { "AssignedGroupId" });
            DropIndex("dbo.FaultMeasureRels", new[] { "FaultMeasureId" });
            DropIndex("dbo.FaultMeasureRels", new[] { "FaultId" });
            DropIndex("dbo.T_BD_FaultGroup", new[] { "ParentId" });
            DropTable("dbo.T_WO_WorkOrderMaterialEntry",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkOrderMaterialEntry_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_WO_WorkOrderFaultEntry",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkOrderFaultEntry_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_WO_WorkOrderBill",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkOrderBill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.T_BD_FaultMeasure",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultMeasure_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_FaultReason",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultReason_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_FaultReasonRel",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultReasonRel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_Fault",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Fault_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.FaultMeasureRels",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultMeasureRel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_FaultGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
