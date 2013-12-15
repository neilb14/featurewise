using System;
using System.Linq;
using GF.FeatureWise.Services.Models;
using Xunit;

namespace Tests.Models
{
    public class GenerateTimeSeriesTest
    {
        [Fact]
        public void ShouldGroupGeneratedTimeSeriesByDate()
        {
            var userEvents = new[]
                {
                    new UserEvent {At = new DateTime(2013, 07, 17), Feature = "Moose", Type = "tick"},
                    new UserEvent {At = new DateTime(2013, 07, 17), Feature = "Moose", Type = "tick"},
                    new UserEvent {At = new DateTime(2013, 07, 18), Feature = "Moose", Type = "tick"}
                };
            var results = new GenerateTimeSeries().Generate(userEvents);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void ShouldGroupGenerateTimeSeriesByFeatureAndDate()
        {
            var userEvents = new[]
                {
                    new UserEvent {At = new DateTime(2013, 07, 17), Feature = "Moose", Type = "tick"},
                    new UserEvent {At = new DateTime(2013, 07, 17), Feature = "Moose", Type = "tick"},
                    new UserEvent {At = new DateTime(2013, 07, 17), Feature = "Beaver", Type = "tick"},
                    new UserEvent {At = new DateTime(2013, 07, 18), Feature = "Moose", Type = "tick"},
                    new UserEvent {At = new DateTime(2013, 07, 18), Feature = "Beaver", Type = "tick"}
                };
            var results = new GenerateTimeSeries().Generate(userEvents);
            Assert.Equal(4, results.Count());
        }
    }
}
