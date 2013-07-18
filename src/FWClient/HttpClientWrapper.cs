using System.Net.Http;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class HttpClientWrapper : IHttpClient
    {        
        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {   
            using(var client = new HttpClient()) 
                return await client.PostAsync(requestUri, content);
        }
    }
}