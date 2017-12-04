namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class www : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentArchives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(maxLength: 128),
                        SerialNo = c.String(),
                        Office = c.String(),
                        OfficePerson = c.String(),
                        OfficePosition = c.String(),
                        OfficeTel = c.String(),
                        OfficeMobile = c.String(),
                        ServiceTime = c.DateTime(),
                        Warrenty = c.DateTime(),
                        AssignedPersonId = c.Long(),
                        CustomerId = c.String(maxLength: 128),
                        InstallType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.AbpUsers", t => t.AssignedPersonId)
                //.ForeignKey("dbo.t_bd_Customer", t => t.CustomerId)
                //.ForeignKey("dbo.t_bd_product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.AssignedPersonId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.T_WO_WorkOrderBill", "InstallType", c => c.String());
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.EquipmentArchives", "ProductId", "dbo.t_bd_product");
            //DropForeignKey("dbo.EquipmentArchives", "CustomerId", "dbo.t_bd_Customer");
            //DropForeignKey("dbo.EquipmentArchives", "AssignedPersonId", "dbo.AbpUsers");
            DropIndex("dbo.EquipmentArchives", new[] { "CustomerId" });
            DropIndex("dbo.EquipmentArchives", new[] { "AssignedPersonId" });
            DropIndex("dbo.EquipmentArchives", new[] { "ProductId" });
            DropColumn("dbo.T_WO_WorkOrderBill", "InstallType");
            DropTable("dbo.EquipmentArchives");
        }
    }
}
