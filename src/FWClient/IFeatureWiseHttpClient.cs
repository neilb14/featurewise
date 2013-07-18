using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public interface IFeatureWiseHttpClient
    {
        Task<HttpResponseMessage> PostUserEvent(Guid id, string feature, string type, DateTime at);
    }
}