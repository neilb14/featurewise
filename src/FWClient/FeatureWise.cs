using System;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class FeatureWise
    {
        public static FeatureWiseImpl instance = new FeatureWiseImpl(new RetryFailedRequestsDecorator(new FeatureWiseHttpClient(new HostnameProvider(),new HttpClientWrapper()),5));

        public static void Tick(string feature, DateTime timestamp)
        {
            Task.Run(() => instance.Tick(feature, timestamp));            
        } 

        public static void Start(string feature, DateTime timestamp)
        {
            Task.Run(() => instance.Start(feature, timestamp));
        }

        public static void Stop(string feature, DateTime timestamp)
        {
            Task.Run(() => instance.Stop(feature, timestamp));
        }
    }
}