namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dawup2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "DeviceGuid", c => c.String());
            AddColumn("dbo.Devices", "Owner", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Owner");
            DropColumn("dbo.Devices", "DeviceGuid");
        }
    }
}
