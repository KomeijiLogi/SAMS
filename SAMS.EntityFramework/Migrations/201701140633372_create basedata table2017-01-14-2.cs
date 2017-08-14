namespace SAMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class createbasedatatable201701142 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_BD_CustomerLinkMan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactPerson = c.String(maxLength: 50),
                        ContactPersonPost = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        Fax = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Address = c.String(maxLength: 100),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_BD_Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        Number1 = c.String(maxLength: 50),
                        Number2 = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        ProvinceId = c.Int(),
                        CityId = c.Int(),
                        Address = c.String(maxLength: 100),
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
                    { "DynamicFilter_Customer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.CityId)
                .ForeignKey("dbo.Areas", t => t.ProvinceId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Areas",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.T_WO_WorkOrderBill", "BillStatus", c => c.Byte(nullable: false));
            AlterColumn("dbo.T_BD_FaultGroup", "Number", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_FaultGroup", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_FaultGroup", "Description", c => c.String(maxLength: 2048));
            AlterColumn("dbo.T_BD_Fault", "Number", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_Fault", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_Fault", "Description", c => c.String(maxLength: 2048));
            AlterColumn("dbo.T_BD_FaultReason", "Number", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_FaultReason", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_FaultReason", "Description", c => c.String(maxLength: 2048));
            AlterColumn("dbo.T_BD_FaultMeasure", "Number", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_FaultMeasure", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_BD_FaultMeasure", "Description", c => c.String(maxLength: 2048));
            AlterColumn("dbo.T_WO_WorkOrderBill", "Number", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_BD_Customer", "ProvinceId", "dbo.Areas");
            DropForeignKey("dbo.T_BD_Customer", "CityId", "dbo.Areas");
            DropForeignKey("dbo.Areas", "ParentId", "dbo.Areas");
            DropIndex("dbo.Areas", new[] { "ParentId" });
            DropIndex("dbo.T_BD_Customer", new[] { "CityId" });
            DropIndex("dbo.T_BD_Customer", new[] { "ProvinceId" });
            AlterColumn("dbo.T_WO_WorkOrderBill", "Number", c => c.String());
            AlterColumn("dbo.T_BD_FaultMeasure", "Description", c => c.String());
            AlterColumn("dbo.T_BD_FaultMeasure", "Name", c => c.String());
            AlterColumn("dbo.T_BD_FaultMeasure", "Number", c => c.String());
            AlterColumn("dbo.T_BD_FaultReason", "Description", c => c.String());
            AlterColumn("dbo.T_BD_FaultReason", "Name", c => c.String());
            AlterColumn("dbo.T_BD_FaultReason", "Number", c => c.String());
            AlterColumn("dbo.T_BD_Fault", "Description", c => c.String());
            AlterColumn("dbo.T_BD_Fault", "Name", c => c.String());
            AlterColumn("dbo.T_BD_Fault", "Number", c => c.String());
            AlterColumn("dbo.T_BD_FaultGroup", "Description", c => c.String());
            AlterColumn("dbo.T_BD_FaultGroup", "Name", c => c.String());
            AlterColumn("dbo.T_BD_FaultGroup", "Number", c => c.String());
            DropColumn("dbo.T_WO_WorkOrderBill", "BillStatus");
            DropTable("dbo.Areas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Area_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_Customer",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Customer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.T_BD_CustomerLinkMan");
        }
    }
}
