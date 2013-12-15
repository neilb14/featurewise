using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public interface IFeatureRepository
    {
        IEnumerable<Feature> GetAll();
        bool Exists(string name);
        Feature Add(Feature feature);
        Feature Get(string name);     
    }
}
