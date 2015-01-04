using System;
using System.Net.Http;

namespace GF.FeatureWise.Client
{
    public interface IFeatureWiseHttpClient
    {
        HttpResponseMessage PostUserEvent(Guid id, string feature, string type, DateTime at);
        HttpResponseMessage PostUserScope(Guid guid, string feature, string type, DateTime start, DateTime stop);
    }
}