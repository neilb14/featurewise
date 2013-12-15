using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public class GenerateHistogram : IGenerate<Histogram>
    {
        public IEnumerable<Histogram> Generate(IEnumerable<UserEvent> userEvents)
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
    }
}