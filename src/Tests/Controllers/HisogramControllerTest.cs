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
        private Mock<IGenerate<Histogram>> generateHistogram;

        public HisogramControllerTest()
        {
            histogramRepository = new Mock<IHistogramRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            generateHistogram = new Mock<IGenerate<Histogram>>();
            controller = new HistogramController(histogramRepository.Object, userEventRepository.Object, generateHistogram.Object);
        }

        [Fact]
        public void ShouldGenerateTimeSeries()
        {
            var userEvents = new[]
                {
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now}, new UserEvent {Type = "tick", Feature = "Beaver", At = DateTime.Now}, new UserEvent {Type = "tick", Feature = "Goose", At = DateTime.Now},
                };
            userEventRepository.Setup(r => r.GetAll()).Returns(userEvents);
            var expectedHistogram = new Histogram();
            generateHistogram.Setup(g => g.Generate(userEvents)).Returns(new[] {expectedHistogram});
            histogramRepository.Setup(r => r.DeleteAll());
            histogramRepository.Setup(r => r.Add(expectedHistogram));
            var result = controller.Generate() as RedirectResult;
            generateHistogram.VerifyAll();
            histogramRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("/Histogram", result.Url);
        }
    }
}