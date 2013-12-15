using System.Data.Entity;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services
{
    public class ApiDataContext : DbContext
    {
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<TimeSeries> TimeSeries { get; set; }
        public virtual DbSet<Histogram> Histograms { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
    }
}