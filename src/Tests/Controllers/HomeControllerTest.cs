using System.Collections.Generic;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<ITimeSeriesRepository> timeSeriesRepository;
        private readonly HomeController controller;

        public HomeControllerTest()
        {
            timeSeriesRepository = new Mock<ITimeSeriesRepository>();
            controller = new HomeController(timeSeriesRepository.Object);
        }

        [Fact]
        public void GetIndex_ShouldBuildListOfFeaturesWithSparklines()
        {
            timeSeriesRepository.Setup(r => r.GetAll()).Returns(new[]
                {
                    new TimeSeries {Feature = "Moose", Ticks = 10, Starts = 30, Year = 2013, Month = 9, Day = 18},
                    new TimeSeries {Feature = "Moose", Ticks = 11, Starts = 35, Year = 2013, Month = 9, Day = 19},
                    new TimeSeries {Feature = "Moose", Ticks = 12, Starts = 30, Year = 2013, Month = 9, Day = 20}
                });
            controller.Index();
            Dictionary<string, List<int>> map = controller.ViewBag.Features;            
            Assert.Equal(1, map.Count);
            Assert.True(map.ContainsKey("Moose"));
            timeSeriesRepository.VerifyAll();
        }
    }
}