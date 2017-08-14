namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _20170117 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_WO_WorkOrderBill", name: "UserId", newName: "AssignedPersonId");
            RenameIndex(table: "dbo.T_WO_WorkOrderBill", name: "IX_UserId", newName: "IX_AssignedPersonId");
            CreateTable(
                "dbo.T_WO_WorkOrderFaultEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
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
                    { "DynamicFilter_WorkOrderFaultEntryPhoto_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_WO_WorkOrderFaultEntry", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.T_WO_WorkOrderMaterialEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
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
                    { "DynamicFilter_WorkOrderMaterialEntryPhoto_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_WO_WorkOrderMaterialEntry", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.T_BD_Material", "Name", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "Address", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderBill", "CustomerPhone", c => c.String(maxLength: 50));
            AddColumn("dbo.T_WO_WorkOrderBill", "CustomerLinkMan", c => c.String(maxLength: 50));
            AddColumn("dbo.T_WO_WorkOrderBill", "SaleMan", c => c.String(maxLength: 50));
            AddColumn("dbo.T_WO_WorkOrderBill", "SaleManPhone", c => c.String(maxLength: 50));
            AddColumn("dbo.T_WO_WorkOrderBill", "EquipmentId", c => c.Int(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "SerialNo", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderBill", "EquLocation", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderBill", "ServiceType", c => c.Byte(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "Description", c => c.String(maxLength: 2048));
            AddColumn("dbo.T_WO_WorkOrderBill", "PlanStartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "PlanEndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "AuditTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "ReleaseTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "SerialNo", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderMaterialEntry", "Direct", c => c.Byte(nullable: false));
            CreateIndex("dbo.T_WO_WorkOrderBill", "CustomerId");
            CreateIndex("dbo.T_WO_WorkOrderBill", "EquipmentId");
            AddForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.T_BD_Customer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_WO_WorkOrderMaterialEntryPhoto", "ParentId", "dbo.T_WO_WorkOrderMaterialEntry");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntryPhoto", "ParentId", "dbo.T_WO_WorkOrderFaultEntry");
            DropForeignKey("dbo.T_WO_WorkOrderBill", "EquipmentId", "dbo.T_BD_Material");
            DropForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.T_BD_Customer");
            DropIndex("dbo.T_WO_WorkOrderMaterialEntryPhoto", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntryPhoto", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "EquipmentId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "CustomerId" });
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "Direct");
            DropColumn("dbo.T_WO_WorkOrderMaterialEntry", "SerialNo");
            DropColumn("dbo.T_WO_WorkOrderBill", "ReleaseTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "AuditTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "EndTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "StartTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "PlanEndTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "PlanStartTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "Description");
            DropColumn("dbo.T_WO_WorkOrderBill", "ServiceType");
            DropColumn("dbo.T_WO_WorkOrderBill", "EquLocation");
            DropColumn("dbo.T_WO_WorkOrderBill", "SerialNo");
            DropColumn("dbo.T_WO_WorkOrderBill", "EquipmentId");
            DropColumn("dbo.T_WO_WorkOrderBill", "SaleManPhone");
            DropColumn("dbo.T_WO_WorkOrderBill", "SaleMan");
            DropColumn("dbo.T_WO_WorkOrderBill", "CustomerLinkMan");
            DropColumn("dbo.T_WO_WorkOrderBill", "CustomerPhone");
            DropColumn("dbo.T_WO_WorkOrderBill", "Address");
            DropColumn("dbo.T_WO_WorkOrderBill", "CustomerId");
            DropColumn("dbo.T_BD_Material", "Name");
            DropTable("dbo.T_WO_WorkOrderMaterialEntryPhoto",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkOrderMaterialEntryPhoto_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_WO_WorkOrderFaultEntryPhoto",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkOrderFaultEntryPhoto_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            RenameIndex(table: "dbo.T_WO_WorkOrderBill", name: "IX_AssignedPersonId", newName: "IX_UserId");
            RenameColumn(table: "dbo.T_WO_WorkOrderBill", name: "AssignedPersonId", newName: "UserId");
        }
    }
}
