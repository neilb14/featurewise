using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseHttpClient : IFeatureWiseHttpClient
    {
        private readonly IProvideHostname hostnameProvider;
        private readonly IHttpClient client;

        public FeatureWiseHttpClient(IProvideHostname hostnameProvider, IHttpClient client)
        {
            this.hostnameProvider = hostnameProvider;
            this.client = client;
        }

        public async Task<HttpResponseMessage> PostUserEvent(Guid id, string feature, string type, DateTime at)
        {
            var url = String.Format("{0}/api/UserEvents", hostnameProvider.GetHostname());
            var content = new StringContent(JsonContent.Build(id, feature, type, at), Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, content);
            return result;
        }
    }
}