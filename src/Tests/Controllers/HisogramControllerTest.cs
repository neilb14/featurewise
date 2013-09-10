using System;
using System.Web.Mvc;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class HisogramControllerTest
    {
        private readonly HistogramController controller;
        private readonly Mock<IHistogramRepository> histogramRepository;
        private readonly Mock<IUserEventRepository> userEventRepository;

        public HisogramControllerTest()
        {
            histogramRepository = new Mock<IHistogramRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            controller = new HistogramController(histogramRepository.Object, userEventRepository.Object);
        }

        [Fact]
        public void ShouldGenerateTimeSeries()
        {
            userEventRepository.Setup(r => r.GetAll()).Returns(new[]
                {
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now},
                    new UserEvent {Type = "tick", Feature = "Beaver", At = DateTime.Now},
                    new UserEvent {Type = "tick", Feature = "Goose", At = DateTime.Now},
                });
            histogramRepository.Setup(r => r.DeleteAll());
            histogramRepository.Setup(r => r.Add(It.IsAny<Histogram>()));
            var result = controller.Generate() as RedirectResult;
            histogramRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("/Histogram", result.Url);
        }
    }
}