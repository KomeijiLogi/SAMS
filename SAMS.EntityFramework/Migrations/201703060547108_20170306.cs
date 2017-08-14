namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170306 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_WO_Activity", name: "AssignedPerson_Id", newName: "AssignedPersonId");
            RenameColumn(table: "dbo.T_WO_Activity", name: "Operater_Id", newName: "OperaterId");
            RenameIndex(table: "dbo.T_WO_Activity", name: "IX_Operater_Id", newName: "IX_OperaterId");
            RenameIndex(table: "dbo.T_WO_Activity", name: "IX_AssignedPerson_Id", newName: "IX_AssignedPersonId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.T_WO_Activity", name: "IX_AssignedPersonId", newName: "IX_AssignedPerson_Id");
            RenameIndex(table: "dbo.T_WO_Activity", name: "IX_OperaterId", newName: "IX_Operater_Id");
            RenameColumn(table: "dbo.T_WO_Activity", name: "OperaterId", newName: "Operater_Id");
            RenameColumn(table: "dbo.T_WO_Activity", name: "AssignedPersonId", newName: "AssignedPerson_Id");
        }
    }
}
