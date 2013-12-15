namespace GF.FeatureWise.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSparklineToFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Features", "Sparkline", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Features", "Sparkline");
        }
    }
}
