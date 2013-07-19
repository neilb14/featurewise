using System.Data.Entity;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services
{
    public class ApiDataContext : DbContext
    {
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<TimeSeries> TimeSeries { get; set; }
    }
}