namespace apitest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sensorFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorPorts",
                c => new
                    {
                        SensorPortId = c.Int(nullable: false, identity: true),
                        port = c.Int(nullable: false),
                        LightConfig_LightConfigId = c.Int(),
                    })
                .PrimaryKey(t => t.SensorPortId)
                .ForeignKey("dbo.LightConfigs", t => t.LightConfig_LightConfigId)
                .Index(t => t.LightConfig_LightConfigId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorPorts", "LightConfig_LightConfigId", "dbo.LightConfigs");
            DropIndex("dbo.SensorPorts", new[] { "LightConfig_LightConfigId" });
            DropTable("dbo.SensorPorts");
        }
    }
}
