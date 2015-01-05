using System;

namespace GF.FeatureWise.Client
{
    public interface IFeatureWise
    {
        FeatureWiseResponse Tick(string feature, DateTime at);
        FeatureWiseResponse Start(string feature, DateTime at);
        FeatureWiseResponse Stop(string feature, DateTime at);
        IDisposable CreateScope(string feature, DateTime start);
        void CommitScope(string feature, DateTime start, DateTime stop);
    }
}