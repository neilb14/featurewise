using System;

namespace GF.FeatureWise.Client
{
    public class FeatureWise
    {
        public static FeatureWiseImpl instance = new FeatureWiseImpl(null);

        public static FeatureWiseResponse Tick(string feature, DateTime timestamp)
        {
            return instance.Tick(feature, timestamp).Result;
        }        
    }
}