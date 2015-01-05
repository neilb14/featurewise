using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class FeatureWiseScope : IDisposable
    {
        private readonly string feature;
        private readonly DateTime timestamp;
        private readonly FeatureWiseImpl featurewise;

        public FeatureWiseScope(string feature, DateTime timestamp, FeatureWiseImpl featurewise)
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
