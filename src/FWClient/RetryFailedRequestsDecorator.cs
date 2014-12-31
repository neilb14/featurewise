using System;
using System.Net.Http;

namespace GF.FeatureWise.Client
{
    public class RetryFailedRequestsDecorator : IFeatureWiseHttpClient
    {
        private readonly IFeatureWiseHttpClient realClient;
        private readonly int maxRetries;

        public RetryFailedRequestsDecorator(IFeatureWiseHttpClient realClient, int maxRetries)
        {
            this.realClient = realClient;
            this.maxRetries = maxRetries;
        }

        public HttpResponseMessage PostUserEvent(Guid id, string feature, string type, DateTime at)
        {
            var currentTry = 0;
            HttpResponseMessage result = null;
            while (currentTry++ < maxRetries)
            {
                result = realClient.PostUserEvent(id, feature, type, at);
                if (result.IsSuccessStatusCode) return result;
            }
            return result;
        }
    }
}