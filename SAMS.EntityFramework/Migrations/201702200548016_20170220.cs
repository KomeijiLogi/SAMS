namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170220 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.T_WO_WorkOrderMaterialEntry", newName: "T_WO_WorkOrderAccessoryEntry");
            RenameTable(name: "dbo.T_WO_WorkOrderFaultEntryPhoto", newName: "T_WO_WorkOrderPhoto");
            DropForeignKey("dbo.T_WO_WorkOrderMaterialEntryPhoto", "ParentId", "dbo.T_WO_WorkOrderMaterialEntry");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntryPhoto", "ParentId", "dbo.T_WO_WorkOrderFaultEntry");
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultId" });
            DropIndex("dbo.T_WO_WorkOrderPhoto", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderMaterialEntryPhoto", new[] { "ParentId" });
            RenameColumn(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "ParentId", newName: "BillId");
            RenameColumn(table: "dbo.T_WO_WorkOrderPhoto", name: "ParentId", newName: "WorkOrderFaultEntry_Id");
            RenameColumn(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "MaterialId", newName: "AccessoryId");
            RenameIndex(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "IX_ParentId", newName: "IX_BillId");
            RenameIndex(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "IX_MaterialId", newName: "IX_AccessoryId");
            CreateTable(
                "dbo.T_WO_Activity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Operater_Id = c.Long(),
                        AssignedPerson_Id = c.Long(),
                        AssignedPerson_Id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_WO_WorkOrderBill", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.Operater_Id)
                .ForeignKey("dbo.AbpUsers", t => t.AssignedPerson_Id)
                .ForeignKey("dbo.AbpUsers", t => t.AssignedPerson_Id1)
                .Index(t => t.BillId)
                .Index(t => t.Operater_Id)
                .Index(t => t.AssignedPerson_Id)
                .Index(t => t.AssignedPerson_Id1);
            
            AddColumn("dbo.T_BD_CustomerLinkMan", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "ContactPerson", c => c.String(maxLength: 50));
            AddColumn("dbo.T_WO_WorkOrderBill", "ContactPersonPost", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "Email", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "DispatchTime", c => c.DateTime());
            AddColumn("dbo.T_WO_WorkOrderBill", "Resolved", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "Evaluation", c => c.Int());
            AddColumn("dbo.T_WO_WorkOrderBill", "ReturnVisitDesc", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "BackCount", c => c.Int(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderPhoto", "BillId", c => c.Int(nullable: false));
            AlterColumn("dbo.T_WO_WorkOrderBill", "BillStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultId", c => c.Int(nullable: false));
            AlterColumn("dbo.T_WO_WorkOrderPhoto", "WorkOrderFaultEntry_Id", c => c.Int());
            CreateIndex("dbo.T_BD_CustomerLinkMan", "CustomerId");
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultId");
            CreateIndex("dbo.T_WO_WorkOrderPhoto", "BillId");
            CreateIndex("dbo.T_WO_WorkOrderPhoto", "WorkOrderFaultEntry_Id");
            //AddForeignKey("dbo.T_BD_CustomerLinkMan", "CustomerId", "dbo.T_BD_Customer", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.T_WO_WorkOrderPhoto", "BillId", "dbo.T_WO_WorkOrderBill", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.T_WO_WorkOrderPhoto", "WorkOrderFaultEntry_Id", "dbo.T_WO_WorkOrderFaultEntry", "Id");
            DropColumn("dbo.T_BD_CustomerLinkMan", "Phone");
            DropColumn("dbo.T_BD_CustomerLinkMan", "Fax");
            DropColumn("dbo.T_BD_CustomerLinkMan", "Address");
            DropColumn("dbo.T_BD_CustomerLinkMan", "LastModificationTime");
            DropColumn("dbo.T_BD_CustomerLinkMan", "LastModifierUserId");
            DropColumn("dbo.T_BD_CustomerLinkMan", "CreationTime");
            DropColumn("dbo.T_BD_CustomerLinkMan", "CreatorUserId");
            DropColumn("dbo.T_WO_WorkOrderBill", "CustomerLinkMan");
            DropColumn("dbo.T_WO_WorkOrderBill", "PlanStartTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "PlanEndTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "StartTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "EndTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "AuditTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "ReleaseTime");
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "NewSerialNo");
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "OldSerialNo");
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "Qty");
            DropTable("dbo.T_WO_WorkOrderMaterialEntryPhoto");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.T_WO_WorkOrderMaterialEntryPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "Qty", c => c.Int(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "OldSerialNo", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderAccessoryEntry", "NewSerialNo", c => c.String(maxLength: 100));
            AddColumn("dbo.T_WO_WorkOrderBill", "ReleaseTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "AuditTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "PlanEndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "PlanStartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_WO_WorkOrderBill", "CustomerLinkMan", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_CustomerLinkMan", "CreatorUserId", c => c.Long());
            AddColumn("dbo.T_BD_CustomerLinkMan", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_BD_CustomerLinkMan", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.T_BD_CustomerLinkMan", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.T_BD_CustomerLinkMan", "Address", c => c.String(maxLength: 100));
            AddColumn("dbo.T_BD_CustomerLinkMan", "Fax", c => c.String(maxLength: 50));
            AddColumn("dbo.T_BD_CustomerLinkMan", "Phone", c => c.String(maxLength: 50));
            DropForeignKey("dbo.T_WO_WorkOrderPhoto", "WorkOrderFaultEntry_Id", "dbo.T_WO_WorkOrderFaultEntry");
            DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault");
            DropForeignKey("dbo.T_WO_WorkOrderPhoto", "BillId", "dbo.T_WO_WorkOrderBill");
            DropForeignKey("dbo.T_WO_Activity", "AssignedPerson_Id1", "dbo.AbpUsers");
            DropForeignKey("dbo.T_WO_Activity", "AssignedPerson_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.T_WO_Activity", "Operater_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.T_WO_Activity", "BillId", "dbo.T_WO_WorkOrderBill");
            DropForeignKey("dbo.T_BD_CustomerLinkMan", "CustomerId", "dbo.T_BD_Customer");
            DropIndex("dbo.T_WO_WorkOrderPhoto", new[] { "WorkOrderFaultEntry_Id" });
            DropIndex("dbo.T_WO_WorkOrderPhoto", new[] { "BillId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultId" });
            DropIndex("dbo.T_WO_Activity", new[] { "AssignedPerson_Id1" });
            DropIndex("dbo.T_WO_Activity", new[] { "AssignedPerson_Id" });
            DropIndex("dbo.T_WO_Activity", new[] { "Operater_Id" });
            DropIndex("dbo.T_WO_Activity", new[] { "BillId" });
            DropIndex("dbo.T_BD_CustomerLinkMan", new[] { "CustomerId" });
            AlterColumn("dbo.T_WO_WorkOrderPhoto", "WorkOrderFaultEntry_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.T_WO_WorkOrderFaultEntry", "FaultId", c => c.Int());
            AlterColumn("dbo.T_WO_WorkOrderBill", "BillStatus", c => c.Byte(nullable: false));
            DropColumn("dbo.T_WO_WorkOrderPhoto", "BillId");
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "BackCount");
            DropColumn("dbo.T_WO_WorkOrderAccessoryEntry", "Count");
            DropColumn("dbo.T_WO_WorkOrderBill", "ReturnVisitDesc");
            DropColumn("dbo.T_WO_WorkOrderBill", "Evaluation");
            DropColumn("dbo.T_WO_WorkOrderBill", "Resolved");
            DropColumn("dbo.T_WO_WorkOrderBill", "DispatchTime");
            DropColumn("dbo.T_WO_WorkOrderBill", "Email");
            DropColumn("dbo.T_WO_WorkOrderBill", "ContactPersonPost");
            DropColumn("dbo.T_WO_WorkOrderBill", "ContactPerson");
            DropColumn("dbo.T_BD_CustomerLinkMan", "CustomerId");
            DropTable("dbo.T_WO_Activity");
            RenameIndex(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "IX_AccessoryId", newName: "IX_MaterialId");
            RenameIndex(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "IX_BillId", newName: "IX_ParentId");
            RenameColumn(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "AccessoryId", newName: "MaterialId");
            RenameColumn(table: "dbo.T_WO_WorkOrderPhoto", name: "WorkOrderFaultEntry_Id", newName: "ParentId");
            RenameColumn(table: "dbo.T_WO_WorkOrderAccessoryEntry", name: "BillId", newName: "ParentId");
            CreateIndex("dbo.T_WO_WorkOrderMaterialEntryPhoto", "ParentId");
            CreateIndex("dbo.T_WO_WorkOrderPhoto", "ParentId");
            CreateIndex("dbo.T_WO_WorkOrderFaultEntry", "FaultId");
            AddForeignKey("dbo.T_WO_WorkOrderFaultEntryPhoto", "ParentId", "dbo.T_WO_WorkOrderFaultEntry", "Id", cascadeDelete: true);
            AddForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault", "Id");
            AddForeignKey("dbo.T_WO_WorkOrderMaterialEntryPhoto", "ParentId", "dbo.T_WO_WorkOrderMaterialEntry", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.T_WO_WorkOrderPhoto", newName: "T_WO_WorkOrderFaultEntryPhoto");
            RenameTable(name: "dbo.T_WO_WorkOrderAccessoryEntry", newName: "T_WO_WorkOrderMaterialEntry");
        }
    }
}
