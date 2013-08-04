using System.Data.Entity.Migrations;

namespace GF.FeatureWise.Services.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApiDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApiDataContext context)
        {
            // do nothing
        }
    }
}
