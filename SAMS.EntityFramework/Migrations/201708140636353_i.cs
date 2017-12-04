namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class i : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.t_bd_accessory",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Model = c.String(),
                        Unit = c.String(),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_WO_Activity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperaterId = c.Long(),
                        BillId = c.Int(nullable: false),
                        AssignedPersonId = c.Long(),
                        Log = c.String(),
                        Name = c.String(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.AbpUsers", t => t.AssignedPersonId)
                //.ForeignKey("dbo.T_WO_WorkOrderBill", t => t.BillId, cascadeDelete: true)
                //.ForeignKey("dbo.AbpUsers", t => t.OperaterId)
                .Index(t => t.OperaterId)
                .Index(t => t.BillId)
                .Index(t => t.AssignedPersonId);
            
            CreateTable(
                "dbo.T_WO_WorkOrderBill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        CustomerId = c.String(maxLength: 128),
                        CustomerName = c.String(),
                        CustomerCode = c.String(),
                        CustomerArea = c.String(),
                        Address = c.String(maxLength: 100),
                        CustomerPhone = c.String(maxLength: 50),
                        IssueBill = c.String(),
                        SaleOrg = c.String(),
                        SaleMan = c.String(maxLength: 50),
                        SaleManPhone = c.String(maxLength: 50),
                        SaleManNumber = c.String(),
                        ProductId = c.String(maxLength: 128),
                        SerialNo = c.String(maxLength: 100),
                        ServiceType = c.Byte(nullable: false),
                        Description = c.String(maxLength: 2048),
                        AssignedPersonId = c.Long(),
                        DispatchTime = c.DateTime(),
                        Office = c.String(),
                        OfficePerson = c.String(),
                        OfficePosition = c.String(),
                        OfficeTel = c.String(),
                        OfficeMobile = c.String(),
                        ServiceTime = c.DateTime(),
                        Warrenty = c.DateTime(),
                        GuaranteedState = c.Boolean(nullable: false),
                        BillStatus = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        ServiceEvalutionId = c.Int(),
                        YUNMsg = c.String(),
                        Faultap = c.String(),
                        Dealfa = c.String(),
                        TrafficUrban = c.String(),
                        TrafficLong = c.String(),
                        HotelEx = c.String(),
                        Supply = c.String(),
                        OtherEx = c.String(),
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
                //.ForeignKey("dbo.AbpUsers", t => t.AssignedPersonId)
                //.ForeignKey("dbo.t_bd_Customer", t => t.CustomerId)
                //.ForeignKey("dbo.t_bd_product", t => t.ProductId)
                //.ForeignKey("dbo.T_WO_ServiceEvalution", t => t.ServiceEvalutionId)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId)
                .Index(t => t.AssignedPersonId)
                .Index(t => t.ServiceEvalutionId);
            
            CreateTable(
                "dbo.T_WO_WorkOrderAccessoryEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        AccessoryId = c.String(maxLength: 128),
                        Count = c.Int(nullable: false),
                        BackCount = c.Int(nullable: false),
                        NewSerials = c.String(),
                        OldSerials = c.String(),
                    })
                .PrimaryKey(t => t.Id)
               // .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId)
               // .ForeignKey("dbo.T_WO_WorkOrderBill", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId)
                .Index(t => t.AccessoryId);
            
            CreateTable(
                "dbo.t_bd_Customer",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Code = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        ProvinceName = c.String(),
                        ProvinceId = c.String(),
                        CityName = c.String(),
                        CityId = c.String(),
                        Address = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_WO_WorkOrderFaultEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        FaultId = c.Int(nullable: false),
                        FaultReasonId = c.Int(),
                        FaultMeasureId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
               // .ForeignKey("dbo.T_WO_WorkOrderBill", t => t.ParentId, cascadeDelete: true)
               // .ForeignKey("dbo.T_BD_Fault", t => t.FaultId, cascadeDelete: true)
              //  .ForeignKey("dbo.T_BD_FaultMeasure", t => t.FaultMeasureId)
              //  .ForeignKey("dbo.T_BD_FaultReason", t => t.FaultReasonId)
                .Index(t => t.ParentId)
                .Index(t => t.FaultId)
                .Index(t => t.FaultReasonId)
                .Index(t => t.FaultMeasureId);
            
            CreateTable(
                "dbo.T_BD_Fault",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        AssignedGroupId = c.Int(nullable: false),
                        Description = c.String(maxLength: 2048),
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
               // .ForeignKey("dbo.T_BD_FaultGroup", t => t.AssignedGroupId, cascadeDelete: true)
                .Index(t => t.AssignedGroupId);
            
            CreateTable(
                "dbo.T_BD_FaultGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        ParentId = c.Int(),
                        Description = c.String(maxLength: 2048),
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
              //  .ForeignKey("dbo.T_BD_FaultGroup", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.T_BD_FaultMeasureRel",
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
             //   .ForeignKey("dbo.T_BD_Fault", t => t.FaultId, cascadeDelete: true)
             //   .ForeignKey("dbo.T_BD_FaultMeasure", t => t.FaultMeasureId, cascadeDelete: true)
                .Index(t => t.FaultId)
                .Index(t => t.FaultMeasureId);
            
            CreateTable(
                "dbo.T_BD_FaultMeasure",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 2048),
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
               // .ForeignKey("dbo.T_BD_Fault", t => t.FaultId, cascadeDelete: true)
               // .ForeignKey("dbo.T_BD_FaultReason", t => t.FaultReasonId, cascadeDelete: true)
                .Index(t => t.FaultId)
                .Index(t => t.FaultReasonId);
            
            CreateTable(
                "dbo.T_BD_FaultReason",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 2048),
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
                "dbo.T_WO_WorkOrderPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(maxLength: 255),
                        FileType = c.String(),
                        BillId = c.Int(nullable: false),
                        WorkOrderFaultEntry_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
              //  .ForeignKey("dbo.T_WO_WorkOrderBill", t => t.BillId, cascadeDelete: true)
              //  .ForeignKey("dbo.T_WO_WorkOrderFaultEntry", t => t.WorkOrderFaultEntry_Id)
                .Index(t => t.BillId)
                .Index(t => t.WorkOrderFaultEntry_Id);
            
            CreateTable(
                "dbo.t_bd_product",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Model = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_WO_ServiceEvalution",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Resolved = c.Boolean(nullable: false),
                        Evaluation = c.Int(),
                        ReturnVisitDesc = c.String(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.t_bd_bom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(maxLength: 128),
                        AccessoryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
               // .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId)
               // .ForeignKey("dbo.t_bd_product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.AccessoryId);
            
            CreateTable(
                "dbo.t_inv_OrgStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessoryId = c.String(maxLength: 128),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId)
                .Index(t => t.AccessoryId);
            
            CreateTable(
                "dbo.t_inv_StaffStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AccessoryId = c.String(maxLength: 128),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId)
                //.ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AccessoryId);
            
            CreateTable(
                "dbo.T_inv_StockBillEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessoryId = c.String(maxLength: 128),
                        Count = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
              //  .ForeignKey("dbo.t_bd_accessory", t => t.AccessoryId)
              //  .ForeignKey("dbo.T_inv_StockBill", t => t.BillId, cascadeDelete: true)
                .Index(t => t.AccessoryId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.T_inv_StockBill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockType = c.Int(nullable: false),
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
              //  .ForeignKey("dbo.AbpUsers", t => t.StraffId)
                .Index(t => t.StraffId);
            
            AddColumn("dbo.AbpUsers", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
        //    DropForeignKey("dbo.T_inv_StockBill", "StraffId", "dbo.AbpUsers");
        //    DropForeignKey("dbo.T_inv_StockBillEntry", "BillId", "dbo.T_inv_StockBill");
        //    DropForeignKey("dbo.T_inv_StockBillEntry", "AccessoryId", "dbo.t_bd_accessory");
        //    DropForeignKey("dbo.t_inv_StaffStock", "UserId", "dbo.AbpUsers");
        //    DropForeignKey("dbo.t_inv_StaffStock", "AccessoryId", "dbo.t_bd_accessory");
        //    DropForeignKey("dbo.t_inv_OrgStock", "AccessoryId", "dbo.t_bd_accessory");
        //    DropForeignKey("dbo.t_bd_bom", "ProductId", "dbo.t_bd_product");
        //    DropForeignKey("dbo.t_bd_bom", "AccessoryId", "dbo.t_bd_accessory");
        //    DropForeignKey("dbo.T_WO_Activity", "OperaterId", "dbo.AbpUsers");
        //    DropForeignKey("dbo.T_WO_WorkOrderBill", "ServiceEvalutionId", "dbo.T_WO_ServiceEvalution");
        //    DropForeignKey("dbo.T_WO_WorkOrderBill", "ProductId", "dbo.t_bd_product");
        //    DropForeignKey("dbo.T_WO_WorkOrderPhoto", "WorkOrderFaultEntry_Id", "dbo.T_WO_WorkOrderFaultEntry");
        //    DropForeignKey("dbo.T_WO_WorkOrderPhoto", "BillId", "dbo.T_WO_WorkOrderBill");
        //    DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultReasonId", "dbo.T_BD_FaultReason");
        //    DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultMeasureId", "dbo.T_BD_FaultMeasure");
        //    DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "FaultId", "dbo.T_BD_Fault");
        //    DropForeignKey("dbo.T_BD_FaultReasonRel", "FaultReasonId", "dbo.T_BD_FaultReason");
        //    DropForeignKey("dbo.T_BD_FaultReasonRel", "FaultId", "dbo.T_BD_Fault");
        //    DropForeignKey("dbo.T_BD_FaultMeasureRel", "FaultMeasureId", "dbo.T_BD_FaultMeasure");
        //    DropForeignKey("dbo.T_BD_FaultMeasureRel", "FaultId", "dbo.T_BD_Fault");
        //    DropForeignKey("dbo.T_BD_Fault", "AssignedGroupId", "dbo.T_BD_FaultGroup");
        //    DropForeignKey("dbo.T_BD_FaultGroup", "ParentId", "dbo.T_BD_FaultGroup");
        //    DropForeignKey("dbo.T_WO_WorkOrderFaultEntry", "ParentId", "dbo.T_WO_WorkOrderBill");
        //    DropForeignKey("dbo.T_WO_WorkOrderBill", "CustomerId", "dbo.t_bd_Customer");
        //    DropForeignKey("dbo.T_WO_WorkOrderBill", "AssignedPersonId", "dbo.AbpUsers");
        //    DropForeignKey("dbo.T_WO_Activity", "BillId", "dbo.T_WO_WorkOrderBill");
        //    DropForeignKey("dbo.T_WO_WorkOrderAccessoryEntry", "BillId", "dbo.T_WO_WorkOrderBill");
        //    DropForeignKey("dbo.T_WO_WorkOrderAccessoryEntry", "AccessoryId", "dbo.t_bd_accessory");
        //    DropForeignKey("dbo.T_WO_Activity", "AssignedPersonId", "dbo.AbpUsers");
            DropIndex("dbo.T_inv_StockBill", new[] { "StraffId" });
            DropIndex("dbo.T_inv_StockBillEntry", new[] { "BillId" });
            DropIndex("dbo.T_inv_StockBillEntry", new[] { "AccessoryId" });
            DropIndex("dbo.t_inv_StaffStock", new[] { "AccessoryId" });
            DropIndex("dbo.t_inv_StaffStock", new[] { "UserId" });
            DropIndex("dbo.t_inv_OrgStock", new[] { "AccessoryId" });
            DropIndex("dbo.t_bd_bom", new[] { "AccessoryId" });
            DropIndex("dbo.t_bd_bom", new[] { "ProductId" });
            DropIndex("dbo.T_WO_WorkOrderPhoto", new[] { "WorkOrderFaultEntry_Id" });
            DropIndex("dbo.T_WO_WorkOrderPhoto", new[] { "BillId" });
            DropIndex("dbo.T_BD_FaultReasonRel", new[] { "FaultReasonId" });
            DropIndex("dbo.T_BD_FaultReasonRel", new[] { "FaultId" });
            DropIndex("dbo.T_BD_FaultMeasureRel", new[] { "FaultMeasureId" });
            DropIndex("dbo.T_BD_FaultMeasureRel", new[] { "FaultId" });
            DropIndex("dbo.T_BD_FaultGroup", new[] { "ParentId" });
            DropIndex("dbo.T_BD_Fault", new[] { "AssignedGroupId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultMeasureId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultReasonId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "FaultId" });
            DropIndex("dbo.T_WO_WorkOrderFaultEntry", new[] { "ParentId" });
            DropIndex("dbo.T_WO_WorkOrderAccessoryEntry", new[] { "AccessoryId" });
            DropIndex("dbo.T_WO_WorkOrderAccessoryEntry", new[] { "BillId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "ServiceEvalutionId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "AssignedPersonId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "ProductId" });
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "CustomerId" });
            DropIndex("dbo.T_WO_Activity", new[] { "AssignedPersonId" });
            DropIndex("dbo.T_WO_Activity", new[] { "BillId" });
            DropIndex("dbo.T_WO_Activity", new[] { "OperaterId" });
            DropColumn("dbo.AbpUsers", "Mobile");
            DropTable("dbo.T_inv_StockBill",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_StockBill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_inv_StockBillEntry");
            DropTable("dbo.t_inv_StaffStock");
            DropTable("dbo.t_inv_OrgStock");
            DropTable("dbo.t_bd_bom");
            DropTable("dbo.T_WO_ServiceEvalution");
            DropTable("dbo.t_bd_product");
            DropTable("dbo.T_WO_WorkOrderPhoto");
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
            DropTable("dbo.T_BD_FaultMeasure",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultMeasure_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_FaultMeasureRel",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultMeasureRel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_FaultGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FaultGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_Fault",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Fault_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_WO_WorkOrderFaultEntry");
            DropTable("dbo.t_bd_Customer");
            DropTable("dbo.T_WO_WorkOrderAccessoryEntry");
            DropTable("dbo.T_WO_WorkOrderBill",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkOrderBill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_WO_Activity");
            DropTable("dbo.t_bd_accessory");
        }
    }
}
