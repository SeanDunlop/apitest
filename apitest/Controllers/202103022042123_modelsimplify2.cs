/*
 * this is an old migration file that needed to go away so i could update remote db
 * I don't want to delete it because entity framework is black magic already
namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsimplify2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "device_DeviceId", "dbo.Devices");
            DropIndex("dbo.Schedules", new[] { "device_DeviceId" });
            DropColumn("dbo.Schedules", "device_DeviceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "device_DeviceId", c => c.Int());
            CreateIndex("dbo.Schedules", "device_DeviceId");
            AddForeignKey("dbo.Schedules", "device_DeviceId", "dbo.Devices", "DeviceId");
        }
    }
}
*/