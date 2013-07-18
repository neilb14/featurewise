using System.Configuration;

namespace GF.FeatureWise.Client
{
    public class HostnameProvider : IProvideHostname
    {
        public string GetHostname()
        {
            return ConfigurationManager.AppSettings["FeatureWise.Hostname"] ?? @"http://localhost";            
        }
    }
}