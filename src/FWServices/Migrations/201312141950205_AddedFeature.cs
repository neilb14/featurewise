namespace GF.FeatureWise.Services.Migrations
{    
    using System.Data.Entity.Migrations;
    
    public partial class AddedFeature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable:false),
                    Group = c.String(),
                    Notes = c.String(),
                    LastStart = c.DateTime(),
                    CreatedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Features");
        }
    }
}
