using System;
using System.Web.Mvc;
using GF.FeatureWise.Services;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class TimeSeriesControllerTest
    {
        private readonly TimeSeriesController controller;
        private readonly Mock<ITimeSeriesRepository> timeSeriesRepository;        
        private readonly Mock<IUserEventRepository> userEventRepository;
        private readonly Mock<IGenerate<TimeSeries>> generateTimeSeries;
        private readonly Mock<ApiDataContext> mockDataContext;
        private readonly Mock<IFeatureRepository> featureRepository;

        public TimeSeriesControllerTest()
        {
            mockDataContext = new Mock<ApiDataContext>();
            timeSeriesRepository = new Mock<ITimeSeriesRepository>();
            featureRepository = new Mock<IFeatureRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            generateTimeSeries = new Mock<IGenerate<TimeSeries>>();
            controller = new TimeSeriesController(mockDataContext.Object, timeSeriesRepository.Object,userEventRepository.Object, generateTimeSeries.Object, featureRepository.Object);
        }

        [Fact]
        public void ShouldGenerateTimeSeries()
        {
            var userEvents = new[]
                {
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now}, 
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now}, 
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now},
                };
            var expectedTimeSeries = new TimeSeries{Feature = "Moose"};
            var feature = new Feature();
            userEventRepository.Setup(r => r.GetAll()).Returns(userEvents);
            generateTimeSeries.Setup(g => g.Generate(userEvents)).Returns(new [] {expectedTimeSeries});
            timeSeriesRepository.Setup(r => r.DeleteAll());
            timeSeriesRepository.Setup(r => r.Add(expectedTimeSeries));
            featureRepository.Setup((r => r.Get("Moose"))).Returns(feature);
            var result = controller.Generate() as RedirectResult;
            generateTimeSeries.VerifyAll();
            timeSeriesRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("/TimeSeries", result.Url);
        }
    }
}