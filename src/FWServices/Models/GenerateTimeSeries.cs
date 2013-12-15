using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public class GenerateTimeSeries : IGenerate<TimeSeries>
    {
        public IEnumerable<TimeSeries> Generate(IEnumerable<UserEvent> userEvents)
        {
            var map = new Dictionary<TimeSeriesKey, TimeSeries>();
            foreach (var userEvent in userEvents)
            {
                var key = userEvent.CreateKey();
                TimeSeries timeSeries = null;
                if (!map.TryGetValue(key, out timeSeries))
                {
                    timeSeries = userEvent.CreateTimeSeries();
                    map.Add(key, timeSeries);
                }
                userEvent.Register(timeSeries);
            }
            return map.Values;
        }
    }
}