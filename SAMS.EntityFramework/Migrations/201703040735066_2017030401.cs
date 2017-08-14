namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017030401 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_WO_Activity", "AssignedPerson_Id1", "dbo.AbpUsers");
            DropIndex("dbo.T_WO_Activity", new[] { "AssignedPerson_Id1" });
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
            
            AddColumn("dbo.T_WO_Activity", "Log", c => c.String());
            AddColumn("dbo.T_WO_Activity", "Name", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "ServiceEvalutionId", c => c.Int());
            CreateIndex("dbo.T_WO_WorkOrderBill", "ServiceEvalutionId");
           // AddForeignKey("dbo.T_WO_WorkOrderBill", "ServiceEvalutionId", "dbo.T_WO_ServiceEvalution", "Id");
            DropColumn("dbo.T_WO_Activity", "Discriminator");
            DropColumn("dbo.T_WO_Activity", "AssignedPerson_Id1");
            DropColumn("dbo.T_WO_WorkOrderBill", "Resolved");
            DropColumn("dbo.T_WO_WorkOrderBill", "Evaluation");
            DropColumn("dbo.T_WO_WorkOrderBill", "ReturnVisitDesc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_WO_WorkOrderBill", "ReturnVisitDesc", c => c.String());
            AddColumn("dbo.T_WO_WorkOrderBill", "Evaluation", c => c.Int());
            AddColumn("dbo.T_WO_WorkOrderBill", "Resolved", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_WO_Activity", "AssignedPerson_Id1", c => c.Long());
            AddColumn("dbo.T_WO_Activity", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.T_WO_WorkOrderBill", "ServiceEvalutionId", "dbo.T_WO_ServiceEvalution");
            DropIndex("dbo.T_WO_WorkOrderBill", new[] { "ServiceEvalutionId" });
            DropColumn("dbo.T_WO_WorkOrderBill", "ServiceEvalutionId");
            DropColumn("dbo.T_WO_Activity", "Name");
            DropColumn("dbo.T_WO_Activity", "Log");
            DropTable("dbo.T_WO_ServiceEvalution");
            CreateIndex("dbo.T_WO_Activity", "AssignedPerson_Id1");
           // AddForeignKey("dbo.T_WO_Activity", "AssignedPerson_Id1", "dbo.AbpUsers", "Id");
        }
    }
}
