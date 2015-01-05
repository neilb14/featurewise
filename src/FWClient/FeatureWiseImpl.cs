using System;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseImpl : IFeatureWise
    {
        private readonly IFeatureWiseHttpClient client;

        public FeatureWiseImpl(IFeatureWiseHttpClient client)
        {
            this.client = client;
        }

        public FeatureWiseResponse Tick(string feature, DateTime at)
        {
            var id = Guid.NewGuid();
            var result = client.PostUserEvent(id, feature, "Tick", at);
            return new FeatureWiseResponse(id, result.Headers.Location, result.StatusCode);
        }

        public FeatureWiseResponse Start(string feature, DateTime at)
        {
            var id = Guid.NewGuid();
            var result = client.PostUserEvent(id, feature, "Start", at);
            return new FeatureWiseResponse(id, result.Headers.Location, result.StatusCode);
        }

        public FeatureWiseResponse Stop(string feature, DateTime at)
        {
            var id = Guid.NewGuid();
            var result = client.PostUserEvent(id, feature, "Stop", at);
            return new FeatureWiseResponse(id, result.Headers.Location, result.StatusCode);
        }

        public IDisposable CreateScope(string feature, DateTime timestamp)
        {
            return new FeatureWiseScope(feature, timestamp, this);
        }

        public void CommitScope(string feature, DateTime start, DateTime stop)
        {
            client.PostUserScope(Guid.NewGuid(), feature, "Scope", start, stop);
        }
    }
}