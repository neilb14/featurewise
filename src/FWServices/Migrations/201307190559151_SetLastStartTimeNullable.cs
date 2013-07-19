namespace GF.FeatureWise.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetLastStartTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSeries", "LastStart", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSeries", "LastStart", c => c.DateTime(nullable: false));
        }
    }
}
