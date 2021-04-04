namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LightConfigs",
                c => new
                    {
                        LightConfigId = c.Int(nullable: false, identity: true),
                        lightPort = c.Int(nullable: false),
                        Schedule_ScheduleId = c.Int(),
                    })
                .PrimaryKey(t => t.LightConfigId)
                .ForeignKey("dbo.Schedules", t => t.Schedule_ScheduleId)
                .Index(t => t.Schedule_ScheduleId);
            
            AddColumn("dbo.Devices", "room_roomId", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "room_roomWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "room_roomHeight", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "Device_DeviceId", c => c.Int());
            CreateIndex("dbo.Schedules", "Device_DeviceId");
            AddForeignKey("dbo.Schedules", "Device_DeviceId", "dbo.Devices", "DeviceId");
            DropColumn("dbo.Schedules", "DeviceId");
            DropColumn("dbo.Schedules", "sensorPort");
            DropColumn("dbo.Schedules", "lightPort");
            DropColumn("dbo.Schedules", "room_roomId");
            DropColumn("dbo.Schedules", "room_roomWidth");
            DropColumn("dbo.Schedules", "room_roomHeight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "room_roomHeight", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "room_roomWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "room_roomId", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "lightPort", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "sensorPort", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "DeviceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Schedules", "Device_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.LightConfigs", "Schedule_ScheduleId", "dbo.Schedules");
            DropIndex("dbo.LightConfigs", new[] { "Schedule_ScheduleId" });
            DropIndex("dbo.Schedules", new[] { "Device_DeviceId" });
            DropColumn("dbo.Schedules", "Device_DeviceId");
            DropColumn("dbo.Devices", "room_roomHeight");
            DropColumn("dbo.Devices", "room_roomWidth");
            DropColumn("dbo.Devices", "room_roomId");
            DropTable("dbo.LightConfigs");
        }
    }
}
