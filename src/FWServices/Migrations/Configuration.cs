using FWServices.Models;

namespace FWServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext.ApiDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext.ApiDataContext context)
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
