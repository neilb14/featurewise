using System;
using System.Web.Mvc;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class HisogramControllerTest
    {
        private readonly HistogramController controller;
        private readonly Mock<IHistogramRepository> histogramRepository;
        private readonly Mock<IUserEventRepository> userEventRepository;
        private readonly Mock<IGenerate<Histogram>> generateHistogram;
        private readonly Mock<IFeatureRepository> featureRepository;
        private readonly Mock<IBackgroundJobClient> backgroundJobClient;
        private readonly UserEvent[] userEvents;

        public HisogramControllerTest()
        {
            histogramRepository = new Mock<IHistogramRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            generateHistogram = new Mock<IGenerate<Histogram>>();
            featureRepository = new Mock<IFeatureRepository>();
            backgroundJobClient = new Mock<IBackgroundJobClient>();
            userEvents = new[]
                {
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now}, new UserEvent {Type = "tick", Feature = "Beaver", At = DateTime.Now}, new UserEvent {Type = "tick", Feature = "Goose", At = DateTime.Now},
                };
            controller = new HistogramController(histogramRepository.Object, userEventRepository.Object, generateHistogram.Object,featureRepository.Object, backgroundJobClient.Object);
        }

        [Fact]
        public void ShouldGenerateHistogramReport()
        {
            userEventRepository.Setup(r => r.GetAll()).Returns(userEvents);
            var expectedHistogram = new Histogram();
            generateHistogram.Setup(g => g.Generate(userEvents)).Returns(new[] {expectedHistogram});
            histogramRepository.Setup(r => r.DeleteAll());
            histogramRepository.Setup(r => r.Add(expectedHistogram));
            featureRepository.Setup(r => r.GetAll()).Returns(new Feature[0]);
            controller.GenerateReport();
            generateHistogram.VerifyAll();
            histogramRepository.VerifyAll();
        }

        [Fact]
        public void ShouldGenerateHistogramReportInBackground_AndRedirectToHistogramIndex()
        {
            var result = controller.Generate() as RedirectToRouteResult;
            backgroundJobClient.Verify(x => x.Create(It.Is<Job>(job => job.Method.Name == "GenerateReport"), It.IsAny<EnqueuedState>()));
            Assert.NotNull(result);
            Assert.Equal("Histogram", result.RouteValues["controller"]);
            Assert.Equal("Index", result.RouteValues["action"]);
        }


    }
}