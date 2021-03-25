namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedulePeriods", "startTime_hours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTime_minutes", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTime_hours", c => c.Int(nullable: false));
            AddColumn("dbo.SchedulePeriods", "endTime_minutes", c => c.Int(nullable: false));
            DropColumn("dbo.SchedulePeriods", "startTime");
            DropColumn("dbo.SchedulePeriods", "endTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchedulePeriods", "endTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.SchedulePeriods", "startTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.SchedulePeriods", "endTime_minutes");
            DropColumn("dbo.SchedulePeriods", "endTime_hours");
            DropColumn("dbo.SchedulePeriods", "startTime_minutes");
            DropColumn("dbo.SchedulePeriods", "startTime_hours");
        }
    }
}
