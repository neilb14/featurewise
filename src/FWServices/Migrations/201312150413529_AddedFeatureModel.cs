namespace GF.FeatureWise.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotSure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Group = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Features");
        }
    }
}
