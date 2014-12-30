using System;
using System.Net.Http;
using GF.FeatureWise.Client;
using Moq;
using Xunit;

namespace Tests.Client
{
    public class FeatureWiseHttpClientTest
    {
        private readonly Mock<IProvideHostname> hostnameProvider;
        private readonly Mock<IHttpClient> httpClient;

        public FeatureWiseHttpClientTest()
        {
            hostnameProvider = new Mock<IProvideHostname>();           
            httpClient = new Mock<IHttpClient>();                        
        }

        [Fact]
        public void ShouldPostUserEvent()
        {
            var id = Guid.NewGuid();
            var at = DateTime.UtcNow;
            hostnameProvider.Setup(p => p.GetHostname()).Returns("http://localhost");
            var responseMessage = new HttpResponseMessage();
            httpClient.Setup(c => c.Post("http://localhost/api/UserEvents", It.IsAny<HttpContent>())).Returns(responseMessage);
            new FeatureWiseHttpClient(hostnameProvider.Object, httpClient.Object).PostUserEvent(id, "Moose","Tick",at);
            httpClient.VerifyAll();
        }
    }
}