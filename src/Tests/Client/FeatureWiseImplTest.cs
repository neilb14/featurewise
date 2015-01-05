using System;
using System.Net;
using System.Net.Http;
using GF.FeatureWise.Client;
using Moq;
using Xunit;

namespace Tests.Client
{
    public class FeatureWiseImplTest
    {
        private readonly Mock<IFeatureWiseHttpClient> client;
        private readonly FeatureWiseImpl featureWise;

        public FeatureWiseImplTest()
        {
            client = new Mock<IFeatureWiseHttpClient>(MockBehavior.Strict);
            featureWise = new FeatureWiseImpl(client.Object);
        }

        [Fact]
        public void ShouldSendTickEvent()
        {
            DateTime at = DateTime.UtcNow;
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri("http://localhost/api/UserEvents/12345");
            client.Setup(c => c.PostUserEvent(It.IsAny<Guid>(), "Moose", "Tick", It.IsAny<DateTime>()))
                .Returns(httpResponseMessage);
            FeatureWiseResponse response = featureWise.Tick("Moose", at);
            Assert.NotNull(response.Id);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("http://localhost/api/UserEvents/12345", response.Location.ToString());
        }

        [Fact]
        public void ShouldSendStartEvent()
        {
            DateTime at = DateTime.UtcNow;
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri("http://localhost/api/UserEvents/12345");
            client.Setup(c => c.PostUserEvent(It.IsAny<Guid>(), "Moose", "Start", It.IsAny<DateTime>()))
                .Returns(httpResponseMessage);
            FeatureWiseResponse response = featureWise.Start("Moose", at);
            Assert.NotNull(response.Id);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("http://localhost/api/UserEvents/12345", response.Location.ToString());
        }

        [Fact]
        public void ShouldSendStopEvent()
        {
            DateTime at = DateTime.UtcNow;
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri("http://localhost/api/UserEvents/12345");
            client.Setup(c => c.PostUserEvent(It.IsAny<Guid>(), "Moose", "Stop", It.IsAny<DateTime>()))
                .Returns(httpResponseMessage);
            FeatureWiseResponse response = featureWise.Stop("Moose", at);
            Assert.NotNull(response.Id);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("http://localhost/api/UserEvents/12345", response.Location.ToString());
        }
    }
}