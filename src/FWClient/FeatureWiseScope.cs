using System;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseScope : IDisposable
    {
        private readonly string feature;
        private readonly DateTime timestamp;
        private readonly IFeatureWise featurewise;

        public FeatureWiseScope(string feature, DateTime timestamp, IFeatureWise featurewise)
        {
            this.feature = feature;
            this.timestamp = timestamp;
            this.featurewise = featurewise;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Task.Run(()=>featurewise.CommitScope(feature, timestamp, DateTime.Now));
            }
        }
    }
}
