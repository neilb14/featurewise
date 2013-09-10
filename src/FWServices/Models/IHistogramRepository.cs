using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public interface IHistogramRepository
    {
        void DeleteAll();
        Histogram Add(Histogram histogram);
        IEnumerable<Histogram> GetAll();
    }
}
