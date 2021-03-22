namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubObj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedulePeriods",
                c => new
                    {
                        SchedulePeriodId = c.Int(nullable: false, identity: true),
                        startTime = c.DateTime(nullable: false),
                        endTime = c.DateTime(nullable: false),
                        intensity = c.Int(nullable: false),
                        Schedule_ScheduleId = c.Int(),
                    })
                .PrimaryKey(t => t.SchedulePeriodId)
                .ForeignKey("dbo.Schedules", t => t.Schedule_ScheduleId)
                .Index(t => t.Schedule_ScheduleId);
            
            AddColumn("dbo.Schedules", "room_roomId", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "room_roomWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "room_roomHeight", c => c.Int(nullable: false));
            DropColumn("dbo.Schedules", "startTime");
            DropColumn("dbo.Schedules", "endTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "endTime", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "startTime", c => c.Int(nullable: false));
            DropForeignKey("dbo.SchedulePeriods", "Schedule_ScheduleId", "dbo.Schedules");
            DropIndex("dbo.SchedulePeriods", new[] { "Schedule_ScheduleId" });
            DropColumn("dbo.Schedules", "room_roomHeight");
            DropColumn("dbo.Schedules", "room_roomWidth");
            DropColumn("dbo.Schedules", "room_roomId");
            DropTable("dbo.SchedulePeriods");
        }
    }
}
