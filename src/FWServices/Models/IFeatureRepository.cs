using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.FeatureWise.Services.Models
{
    public interface IFeatureRepository
    {
        IEnumerable<Feature> GetAll();
    }
}
