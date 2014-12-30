using System.Net.Http;

namespace GF.FeatureWise.Client
{
    public class HttpClientWrapper : IHttpClient
    {        
        public HttpResponseMessage Post(string requestUri, HttpContent content)
        {   
            using(var client = new HttpClient()) 
                return client.PostAsync(requestUri, content).Result;
        }
    }
}