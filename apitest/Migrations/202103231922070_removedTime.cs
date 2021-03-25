namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedulePeriods", "startTimeHours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTimeMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTimeHours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTimeMinutes", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "startTime_hours");
            DropColumn("dbo.SchedulePeriods", "startTime_minutes");
            DropColumn("dbo.SchedulePeriods", "endTime_hours");
            DropColumn("dbo.SchedulePeriods", "endTime_minutes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchedulePeriods", "endTime_minutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTime_hours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTime_minutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTime_hours", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "endTimeMinutes");
            DropColumn("dbo.SchedulePeriods", "endTimeHours");
            DropColumn("dbo.SchedulePeriods", "startTimeMinutes");
            DropColumn("dbo.SchedulePeriods", "startTimeHours");
        }
    }
}
