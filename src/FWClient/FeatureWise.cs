using System;

namespace GF.FeatureWise.Client
{
    public class FeatureWise
    {
        public static FeatureWiseImpl instance = new FeatureWiseImpl(new FeatureWiseHttpClient(new HostnameProvider(),new HttpClientWrapper()));

        public static FeatureWiseResponse Tick(string feature, DateTime timestamp)
        {
            return instance.Tick(feature, timestamp);
        } 

        public static FeatureWiseResponse Start(string feature, DateTime timestamp)
        {
            return instance.Start(feature, timestamp);
        }
    }
}