namespace GF.FeatureWise.Services.Migrations
{    
    using System.Data.Entity.Migrations;
    
    public partial class AddedHistogram : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histograms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Feature = c.String(),
                        Duration = c.Int(nullable: false),
                        AverageDuration = c.Int(nullable: false),
                        Ticks = c.Int(nullable: false),
                        Starts = c.Int(nullable: false),
                        LastStart = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Histograms");
        }
    }
}
