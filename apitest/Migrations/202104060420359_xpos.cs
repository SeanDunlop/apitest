namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "xpos", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "ypos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "ypos");
            DropColumn("dbo.Devices", "xpos");
        }
    }
}
