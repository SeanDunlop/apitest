namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceModels",
                c => new
                    {
                        DeviceModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeviceModelId);
            
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
                .PrimaryKey(t => t.ScheduleModelId)
                .ForeignKey("dbo.DeviceModels", t => t.device_DeviceModelId)
                .Index(t => t.device_DeviceModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleModels", "device_DeviceModelId", "dbo.DeviceModels");
            DropIndex("dbo.ScheduleModels", new[] { "device_DeviceModelId" });
            DropTable("dbo.ScheduleModels");
            DropTable("dbo.DeviceModels");
        }
    }
}
