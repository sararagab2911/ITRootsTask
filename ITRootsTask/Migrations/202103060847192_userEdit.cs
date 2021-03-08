namespace ITRootsTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "OTP", c => c.String());
            AlterColumn("dbo.InvoiceDetails", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Invoices", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Users", "CreatedBy", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "CreatedBy", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "OTP");
            DropColumn("dbo.Users", "IsVerified");
        }
    }
}
