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

        public TimeSeriesControllerTest()
        {
            timeSeriesRepository = new Mock<ITimeSeriesRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            controller = new TimeSeriesController(timeSeriesRepository.Object,userEventRepository.Object);
        }

        [Fact]
        public void ShouldGenerateTimeSeries()
        {
            userEventRepository.Setup(r => r.GetAll()).Returns(new UserEvent[]
                {
                    new UserEvent{Type = "tick",Feature = "Moose",At=DateTime.Now}, 
                    new UserEvent{Type = "tick",Feature = "Beaver",At=DateTime.Now}, 
                    new UserEvent{Type = "tick",Feature = "Goose",At=DateTime.Now}, 
                });
            timeSeriesRepository.Setup(r => r.DeleteAll());
            timeSeriesRepository.Setup(r => r.Add(It.IsAny<TimeSeries>()));
            var result = controller.Generate() as RedirectResult;
            timeSeriesRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("/TimeSeries", result.Url);
        }
    }
}