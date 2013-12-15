using System.Collections.Generic;
using System.Linq;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApiDataContext context;

        public FeatureRepository(ApiDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Feature> GetAll()
        {
            return context.Features.AsEnumerable().OrderBy(f => f.Name);
        }

        public bool Exists(string name)
        {
            return context.Features.Any(f => f.Name == name);
        }

        public Feature Add(Feature feature)
        {            
            context.Features.Add(feature);
            context.SaveChanges();
            return feature;
        }                
    }
}