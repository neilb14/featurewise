using System;
using System.Net.Http;
using System.Text;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseHttpClient : IFeatureWiseHttpClient
    {
        private readonly IHttpClient client;
        private readonly IProvideHostname hostnameProvider;

        public FeatureWiseHttpClient(IProvideHostname hostnameProvider, IHttpClient client)
        {
            this.hostnameProvider = hostnameProvider;
            this.client = client;
        }

        public HttpResponseMessage PostUserEvent(Guid id, string feature, string type, DateTime at)
        {
            var url = String.Format("{0}/api/UserEvents", hostnameProvider.GetHostname());
            var content = new StringContent(JsonContent.BuildEvent(id, feature, type, at), Encoding.UTF8, "application/json");
            return client.Post(url, content);
        }

        public HttpResponseMessage PostUserScope(Guid id, string feature, string type, DateTime start, DateTime stop)
        {
            var url = String.Format("{0}/api/UserScopes", hostnameProvider.GetHostname());
            var content = new StringContent(JsonContent.BuildScope(id, feature, type, start, stop), Encoding.UTF8, "application/json");
            return client.Post(url, content);
        }
    }
}