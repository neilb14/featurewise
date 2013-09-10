using System;
using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public class Histogram : ITrackUserEvents
    {
        public Guid Id { get; set; }
        public string Feature { get; set; }
        public int Duration { get; set; }
        public int AverageDuration { get; set; }
        public int Ticks { get; set; }
        public int Starts { get; set; }
        public DateTime? LastStart { get; set; }
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<Histogram> Generate(IEnumerable<UserEvent> userEvents)
        {
            var map = new Dictionary<string, Histogram>();
            foreach (var userEvent in userEvents)
            {                
                Histogram histogram = null;
                if (!map.TryGetValue(userEvent.Feature, out histogram))
                {
                    histogram = userEvent.CreateHistogram();
                    map.Add(userEvent.Feature, histogram);
                }
                userEvent.Register(histogram);
            }
            return map.Values;
        }

        public virtual void RegisterTick()
        {
            Ticks++;
        }

        public virtual void RegisterStart(DateTime at)
        {
            Starts++;
            LastStart = at;
        }

        public virtual void RegisterStop(DateTime at)
        {
            Duration += (int)(at - LastStart).Value.TotalSeconds;
            AverageDuration = Duration / Starts;
        }
    }
}