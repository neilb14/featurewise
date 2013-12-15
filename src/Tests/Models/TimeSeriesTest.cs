using System;
using GF.FeatureWise.Services.Models;
using Xunit;

namespace Tests.Models
{
    public class TimeSeriesTest
    {
        private const int FIVE_MINUTES = 5*60;
        private const int TEN_MINUTES = 10*60;
        private const int FIFTEEN_MINUTES = 15*60;

        [Fact]
        public void ShouldRegisterTick()
        {
            var timeSeries = new TimeSeries();
            timeSeries.RegisterTick();
            timeSeries.RegisterTick();
            Assert.Equal(2, timeSeries.Ticks);
        }

        [Fact]
        public void ShouldRegisterStartStopEvents()
        {
            var timeSeries = new TimeSeries();            
            RegisterStartStop(timeSeries, new DateTime(2013, 08, 04, 12, 00, 00), FIVE_MINUTES);
            Assert.Equal(1, timeSeries.Starts);
            Assert.Equal(FIVE_MINUTES, timeSeries.Duration);
            Assert.Equal(FIVE_MINUTES, timeSeries.AverageDuration);
        }

        [Fact]
        public void ShouldRegisterSeveralStartStopEvents()
        {
            var timeSeries = new TimeSeries();
            RegisterStartStop(timeSeries, new DateTime(2013, 08, 04, 12, 00, 00), TEN_MINUTES);
            RegisterStartStop(timeSeries, new DateTime(2013, 08, 04, 1, 00, 00), FIVE_MINUTES);
            RegisterStartStop(timeSeries, new DateTime(2013, 08, 04, 2, 00, 00), TEN_MINUTES);
            RegisterStartStop(timeSeries, new DateTime(2013, 08, 04, 3, 00, 00), FIFTEEN_MINUTES);
            Assert.Equal(4, timeSeries.Starts);
            Assert.Equal(40 * 60, timeSeries.Duration);
            Assert.Equal(TEN_MINUTES, timeSeries.AverageDuration);
        }

        private static void RegisterStartStop(TimeSeries timeSeries, DateTime startAt, int duration_in_seconds)
        {
            timeSeries.RegisterStart(startAt);
            timeSeries.RegisterStop(startAt.AddSeconds(duration_in_seconds));
        }
    }
}