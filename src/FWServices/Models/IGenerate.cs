using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public interface IGenerate<T>
    {
        IEnumerable<T> Generate(IEnumerable<UserEvent> userEvents);
    }
}
