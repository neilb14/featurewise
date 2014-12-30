using System;
using System.Web.Mvc;
using GF.FeatureWise.Services;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
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
        private readonly Mock<IBackgroundJobClient> backgroundJobClient;
        private readonly UserEvent[] userEvents;

        public TimeSeriesControllerTest()
        {
            mockDataContext = new Mock<ApiDataContext>();
            timeSeriesRepository = new Mock<ITimeSeriesRepository>();
            featureRepository = new Mock<IFeatureRepository>();
            userEventRepository = new Mock<IUserEventRepository>();
            generateTimeSeries = new Mock<IGenerate<TimeSeries>>();
            backgroundJobClient = new Mock<IBackgroundJobClient>();
            userEvents = new[]
                {
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now},
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now},
                    new UserEvent {Type = "tick", Feature = "Moose", At = DateTime.Now},
                };
            controller = new TimeSeriesController(mockDataContext.Object, timeSeriesRepository.Object,userEventRepository.Object, generateTimeSeries.Object, featureRepository.Object, backgroundJobClient.Object);
        }

        [Fact]
        public void ShouldGenerateTimeSeriesReport()
        {
            var expectedTimeSeries = new TimeSeries{Feature = "Moose"};
            var feature = new Feature();
            userEventRepository.Setup(r => r.GetAll()).Returns(userEvents);
            generateTimeSeries.Setup(g => g.Generate(userEvents)).Returns(new [] {expectedTimeSeries});
            timeSeriesRepository.Setup(r => r.DeleteAll());
            timeSeriesRepository.Setup(r => r.Add(expectedTimeSeries));
            featureRepository.Setup((r => r.Get("Moose"))).Returns(feature);
            controller.GenerateReports();
            generateTimeSeries.VerifyAll();
            timeSeriesRepository.VerifyAll();
            
        }

        [Fact]
        public void ShouldGenerateReportsInBackground_AndRedirectToTimeSeriesIndex()
        {
            var result = controller.Generate() as RedirectToRouteResult;            
            backgroundJobClient.Verify(x => x.Create(It.Is<Job>(job => job.Method.Name == "GenerateReports"),It.IsAny<EnqueuedState>()));
            Assert.NotNull(result);
            Assert.Equal("TimeSeries", result.RouteValues["controller"]);
            Assert.Equal("Index", result.RouteValues["action"]);
        }

    }
}