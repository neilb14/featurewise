using System.Collections.Generic;
using System.IO;

namespace GF.FeatureWise.Services.Models
{
    public interface IParseUserEvents
    {
        IEnumerable<UserEvent> FromString(string contents);
        IEnumerable<UserEvent> FromStream(Stream stream);        
    }
}
