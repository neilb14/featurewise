using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GF.FeatureWise.Client;
using Moq;
using Xunit;

namespace Tests.Client
{    
    public class FeatureWiseImplTest
    {
        private readonly FeatureWiseImpl featureWise;
        private readonly Mock<IFeatureWiseHttpClient> client;

        public FeatureWiseImplTest()
        {            
            client = new Mock<IFeatureWiseHttpClient>(MockBehavior.Strict);         
            featureWise = new FeatureWiseImpl(client.Object);
        }

        [Fact]
        public async void ShouldSendTickEvent()
        {
            var at = DateTime.UtcNow;
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri("http://localhost/api/UserEvents/12345");
            client.Setup(c => c.PostUserEvent(It.IsAny<Guid>(), "Moose", "Tick", It.IsAny<DateTime>())).Returns(new Task<HttpResponseMessage>(()=>httpResponseMessage));
            var response = await featureWise.Tick("Moose", at);
            Assert.NotNull(response.Id);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("http://localhost/api/UserEvents/12345", response.Location.ToString());
        }

        [Fact]
        public async void ShouldSendStartEvent()
        {
            var at = DateTime.UtcNow;
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri("http://localhost/api/UserEvents/12345");
            client.Setup(c => c.PostUserEvent(It.IsAny<Guid>(), "Moose", "Start", It.IsAny<DateTime>())).Returns(new Task<HttpResponseMessage>(() => httpResponseMessage));
            var response = await featureWise.Start("Moose", at);
            Assert.NotNull(response.Id);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("http://localhost/api/UserEvents/12345", response.Location.ToString());
        }

        [Fact]
        public async void ShouldSendStopEvent()
        {
            var at = DateTime.UtcNow;
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri("http://localhost/api/UserEvents/12345");
            client.Setup(c => c.PostUserEvent(It.IsAny<Guid>(), "Moose", "Stop", It.IsAny<DateTime>())).Returns(new Task<HttpResponseMessage>(() => httpResponseMessage));
            var response = await featureWise.Stop("Moose", at);
            Assert.NotNull(response.Id);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("http://localhost/api/UserEvents/12345", response.Location.ToString());
        }


    }
}