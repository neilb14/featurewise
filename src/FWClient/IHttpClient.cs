using System.Net.Http;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}