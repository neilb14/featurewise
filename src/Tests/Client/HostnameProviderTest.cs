using GF.FeatureWise.Client;
using System.Configuration;
using Xunit;

namespace Tests.Client
{
    public class HostnameProviderTest
    {
        [Fact]
        public void ShouldReturnHostnameSetInAppConfig()
        {
            ConfigurationManager.AppSettings["FeatureWise.Hostname"] = "http://somesite:8818";
            Assert.Equal("http://somesite:8818", new HostnameProvider().GetHostname());
        }

        [Fact]
        public void ShouldReturnLocalhost_WhenFeaturewiseHostnameWasNotSetInAppConfig()
        {
            ConfigurationManager.AppSettings["FeatureWise.Hostname"] = null;
            Assert.Equal("http://localhost", new HostnameProvider().GetHostname());
        }
    }
}