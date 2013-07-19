using System.Collections.Generic;
using System.Linq;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services.Repositories
{
    public class TimeSeriesRepository : ITimeSeriesRepository
    {
        private readonly ApiDataContext context;

        public TimeSeriesRepository(ApiDataContext context)
        {
            this.context = context;
        }

        public void DeleteAll()
        {
            var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
            objectContext.ExecuteStoreCommand(string.Format("TRUNCATE TABLE [{0}]", typeof(TimeSeries).Name));
        }

        public TimeSeries Add(TimeSeries timeSeries)
        {
            context.TimeSeries.Add(timeSeries);
            context.SaveChanges();
            return timeSeries;
        }

        public IEnumerable<TimeSeries> GetAll()
        {
            return context.TimeSeries.AsEnumerable();
        }
    }
}