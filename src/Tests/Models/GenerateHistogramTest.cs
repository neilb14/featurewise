using System;
using System.Linq;
using GF.FeatureWise.Services.Models;
using Xunit;

namespace Tests.Models
{
    public class GenerateHistogramTest
    {
        [Fact]
        public void ShouldGenerateHistogramForASingleFeature()
        {
            var results = new GenerateHistogram().Generate(new[]
                {
                    new UserEvent {Feature = "Moose", At = DateTime.UtcNow, Type = "tick"},
                    new UserEvent {Feature = "Moose", At = DateTime.UtcNow, Type = "tick"},
                    new UserEvent {Feature = "Moose", At = DateTime.UtcNow, Type = "tick"}
                });
            Assert.Equal(1, results.Count());
            Assert.Equal(3, results.First().Ticks);
        }

        [Fact]
        public void ShouldGenerateHistogramForMultipleFeatures()
        {
            var results = new GenerateHistogram().Generate(new[]
                {
                    new UserEvent {Feature = "Moose", At = DateTime.UtcNow, Type = "tick"},
                    new UserEvent {Feature = "Hollerado", At = DateTime.UtcNow, Type = "tick"},
                    new UserEvent {Feature = "Moose", At = DateTime.UtcNow, Type = "tick"}
                });
            Assert.Equal(2, results.Count());
            Assert.Equal(2, results.Single(s => s.Feature == "Moose").Ticks);
            Assert.Equal(1, results.Single(s => s.Feature == "Hollerado").Ticks);
        }
    }
}