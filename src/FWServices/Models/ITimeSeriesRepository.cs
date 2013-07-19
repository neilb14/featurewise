using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public interface ITimeSeriesRepository
    {
        void DeleteAll();
        TimeSeries Add(TimeSeries timeSeries);
        IEnumerable<TimeSeries> GetAll();
    }
}