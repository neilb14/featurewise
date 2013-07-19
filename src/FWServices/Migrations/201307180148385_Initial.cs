using System.Data.Entity.Migrations;

namespace GF.FeatureWise.Services.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Feature = c.String(),
                        Type = c.String(),
                        At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserEvents");
        }
    }
}
