using System;

namespace GF.FeatureWise.Services.Models
{
    public class UserEvent
    {
        public Guid Id { get; set; }
        public string Feature { get; set; }
        public string Type { get; set; }
        public DateTime At { get; set; }

        public TimeSeriesKey CreateKey()
        {
            return new TimeSeriesKey(At.Year,At.Month,At.Day,Feature);
        }

        public TimeSeries CreateTimeSeries()
        {
            return new TimeSeries
            {
                Id = Guid.NewGuid(),
                Feature = Feature,
                Year = At.Year,
                Month = At.Month,
                Day = At.Day,
                LastStart = null,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Register(TimeSeries timeSeries)
        {   
            var cleanType = Type.Trim().ToLower();
            if (cleanType == "tick")
                timeSeries.RegisterTick();
            if(cleanType=="start")
                timeSeries.RegisterStart(At);
            if (cleanType == "stop")
                timeSeries.RegisterStop(At);
        }
    }    
}