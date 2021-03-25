namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeBack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedulePeriods", "startTime_hours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTime_minutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTime_hours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTime_minutes", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "startTimeHours");
            DropColumn("dbo.SchedulePeriods", "startTimeMinutes");
            DropColumn("dbo.SchedulePeriods", "endTimeHours");
            DropColumn("dbo.SchedulePeriods", "endTimeMinutes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchedulePeriods", "endTimeMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTimeHours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTimeMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTimeHours", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "endTime_minutes");
            DropColumn("dbo.SchedulePeriods", "endTime_hours");
            DropColumn("dbo.SchedulePeriods", "startTime_minutes");
            DropColumn("dbo.SchedulePeriods", "startTime_hours");
        }
    }
}
