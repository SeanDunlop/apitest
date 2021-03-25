namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dawsonUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "DeviceId", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "delay", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "intensity", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "sensorPort", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "lightPort", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "duration", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "intensity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchedulePeriods", "intensity", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "duration");
            DropColumn("dbo.Schedules", "lightPort");
            DropColumn("dbo.Schedules", "sensorPort");
            DropColumn("dbo.Schedules", "intensity");
            DropColumn("dbo.Schedules", "delay");
            DropColumn("dbo.Schedules", "DeviceId");
        }
    }
}
