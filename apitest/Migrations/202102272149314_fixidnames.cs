namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixidnames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleModels", "device_DeviceModelId", "dbo.DeviceModels");
            DropIndex("dbo.ScheduleModels", new[] { "device_DeviceModelId" });
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        startTime = c.Int(nullable: false),
                        endTime = c.Int(nullable: false),
                        name = c.String(),
                        device_DeviceId = c.Int(),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Devices", t => t.device_DeviceId)
                .Index(t => t.device_DeviceId);
            
            DropTable("dbo.DeviceModels");
            DropTable("dbo.ScheduleModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScheduleModels",
                c => new
                    {
                        ScheduleModelId = c.Int(nullable: false, identity: true),
                        startTime = c.Int(nullable: false),
                        endTime = c.Int(nullable: false),
                        name = c.String(),
                        device_DeviceModelId = c.Int(),
                    })
                .PrimaryKey(t => t.ScheduleModelId);
            
            CreateTable(
                "dbo.DeviceModels",
                c => new
                    {
                        DeviceModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeviceModelId);
            
            DropForeignKey("dbo.Schedules", "device_DeviceId", "dbo.Devices");
            DropIndex("dbo.Schedules", new[] { "device_DeviceId" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Devices");
            CreateIndex("dbo.ScheduleModels", "device_DeviceModelId");
            AddForeignKey("dbo.ScheduleModels", "device_DeviceModelId", "dbo.DeviceModels", "DeviceModelId");
        }
    }
}
