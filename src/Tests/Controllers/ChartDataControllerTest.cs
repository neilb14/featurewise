using System.Linq;
using System.Net.Http;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class ChartDataControllerTest
    {
        private readonly ChartDataController controller;
        private readonly Mock<ITimeSeriesRepository> repository;

        public ChartDataControllerTest()
        {
            var request = new HttpRequestMessage();
            repository = new Mock<ITimeSeriesRepository>(MockBehavior.Strict);
            controller = new ChartDataController(repository.Object){Request = request};
        }

        [Fact]
        public void ShouldGetAllTimeSeriesDataPoints()
        {
            repository.Setup(r => r.GetAll()).Returns(new[] {new TimeSeries(), new TimeSeries(), new TimeSeries()});
            var result = controller.GetChartData();
            Assert.Equal(3, result.Count());
            repository.VerifyAll();
        }
    }
}
