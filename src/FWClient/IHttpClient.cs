using System.Net.Http;

namespace GF.FeatureWise.Client
{
    public interface IHttpClient
    {
        HttpResponseMessage Post(string requestUri, HttpContent content);
    }
}