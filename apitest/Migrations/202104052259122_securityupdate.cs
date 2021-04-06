namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securityupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "UserId");
        }
    }
}
