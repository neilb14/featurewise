namespace GF.FeatureWise.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeSeries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeSeries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Feature = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        AverageDuration = c.Int(nullable: false),
                        Ticks = c.Int(nullable: false),
                        Starts = c.Int(nullable: false),
                        LastStart = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeSeries");
        }
    }
}
