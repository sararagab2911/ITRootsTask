namespace ITRootsTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userEditrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Roles", c => c.String());
            DropColumn("dbo.Users", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Roles");
        }
    }
}
