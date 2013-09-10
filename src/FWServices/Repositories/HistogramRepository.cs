using System.Collections.Generic;
using System.Linq;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services.Repositories
{
    public class HistogramRepository : IHistogramRepository
    {
        private readonly ApiDataContext context;

        public HistogramRepository(ApiDataContext context)
        {
            this.context = context;
        }

        public void DeleteAll()
        {
            var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
            objectContext.ExecuteStoreCommand(string.Format("TRUNCATE TABLE [{0}]", typeof(Histogram).Name + "s"));
        }

        public Histogram Add(Histogram histogram)
        {
            context.Histograms.Add(histogram);
            context.SaveChanges();
            return histogram;
        }

        public IEnumerable<Histogram> GetAll()
        {
            return context.Histograms.AsEnumerable().OrderBy(t=>t.Feature);
        }
    }
}