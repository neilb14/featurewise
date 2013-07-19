using FWServices;
using GF.FeatureWise.Services.Models;
using System;
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
            context.UserEvents.AddOrUpdate(new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Feature = "Monkey",
                    Type = "Tick",
                    At = DateTime.UtcNow
                });
        }
    }
}
