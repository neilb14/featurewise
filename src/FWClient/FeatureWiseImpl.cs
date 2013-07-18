using System;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseImpl
    {
        private readonly IFeatureWiseHttpClient client;

        public FeatureWiseImpl(IFeatureWiseHttpClient client)
        {
            this.client = client;
        }

        public async Task<FeatureWiseResponse> Tick(string feature, DateTime at)
        {
            var id = Guid.NewGuid();
            var result = await client.PostUserEvent(id, feature, "Tick", at);
            return new FeatureWiseResponse(id, result.Headers.Location, result.StatusCode);
        }
    }
}