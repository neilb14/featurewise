using System;
using System.Web.Mvc;
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

        public TimeSeriesControllerTest()
        {
            timeSeriesRepository = new Mock<ITimeSeriesRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            generateTimeSeries = new Mock<IGenerate<TimeSeries>>();
            controller = new TimeSeriesController(timeSeriesRepository.Object,userEventRepository.Object, generateTimeSeries.Object);
        }

        [Fact]
        public void ShouldGenerateTimeSeries()
        {
            var userEvents = new UserEvent[]
                {
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now}, new UserEvent {Type = "tick", Feature = "Beaver", At = DateTime.Now}, new UserEvent {Type = "tick", Feature = "Goose", At = DateTime.Now},
                };
            var expectedTimeSeries = new TimeSeries();
            userEventRepository.Setup(r => r.GetAll()).Returns(userEvents);
            generateTimeSeries.Setup(g => g.Generate(userEvents)).Returns(new [] {expectedTimeSeries});
            timeSeriesRepository.Setup(r => r.DeleteAll());
            timeSeriesRepository.Setup(r => r.Add(expectedTimeSeries));
            var result = controller.Generate() as RedirectResult;
            generateTimeSeries.VerifyAll();
            timeSeriesRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("/TimeSeries", result.Url);
        }
    }
}