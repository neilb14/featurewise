using System;
using System.Net.Http;

namespace GF.FeatureWise.Client
{
    public interface IFeatureWiseHttpClient
    {
        HttpResponseMessage PostUserEvent(Guid id, string feature, string type, DateTime at);
    }
}