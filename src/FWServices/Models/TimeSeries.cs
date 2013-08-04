using System;
using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public class TimeSeries
    {
        public Guid Id { get; set; }
        public string Feature { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Duration { get; set; }
        public int AverageDuration { get; set; }
        public int Ticks { get; set; }
        public int Starts { get; set; }
        public DateTime? LastStart { get; set; }
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<TimeSeries> Generate(IEnumerable<UserEvent> userEvents)
        {
            var map = new Dictionary<TimeSeriesKey,TimeSeries>();
            foreach (var userEvent in userEvents)
            {
                var key = userEvent.CreateKey();
                TimeSeries timeSeries = null;
                if (!map.TryGetValue(key, out timeSeries))
                {
                    timeSeries = userEvent.CreateTimeSeries();
                    map.Add(key,timeSeries);
                }
                userEvent.Register(timeSeries);                
            }
            return map.Values;
        }

        public virtual void RegisterTick()
        {
            Ticks++;
        }

        public virtual void RegisterStart(DateTime at)
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterStop(DateTime at)
        {
            throw new NotImplementedException();
        }
    }
}