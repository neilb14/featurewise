using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class HistogramDataControllerTest
    {
        private readonly HistogramDataController controller;
        private readonly Mock<IHistogramRepository> repository;

        public HistogramDataControllerTest()
        {
            var request = new HttpRequestMessage();
            repository = new Mock<IHistogramRepository>(MockBehavior.Strict);
            controller = new HistogramDataController(repository.Object) {Request = request};
        }

        [Fact]
        public void ShouldGetAllTimeSeriesDataPoints()
        {
            repository.Setup(r => r.GetAll()).Returns(new[] {new Histogram(), new Histogram(), new Histogram()});
            IEnumerable<Histogram> result = controller.GetData();
            Assert.Equal(3, result.Count());
            repository.VerifyAll();
        }
    }
}