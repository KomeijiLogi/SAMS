namespace SAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1170317 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_inv_StockBill", "StockType", c => c.Int(nullable: false));
            DropColumn("dbo.T_inv_StockBill", "BillType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_inv_StockBill", "BillType", c => c.Int(nullable: false));
            DropColumn("dbo.T_inv_StockBill", "StockType");
        }
    }
}
