using System;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Models
{
    public class UserEventTest
    {
        private readonly UserEvent userEvent;

        public UserEventTest()
        {
            userEvent = new UserEvent {Feature = "Moose", Type = "Tick", At = new DateTime(2013, 7, 18)};
        }

        [Fact]
        public void ShouldCreateTimeSeries()
        {
            TimeSeries timeSeries = userEvent.CreateTimeSeries();
            Assert.Equal("Moose", timeSeries.Feature);
            Assert.Equal(2013, timeSeries.Year);
            Assert.Equal(7, timeSeries.Month);
            Assert.Equal(18, timeSeries.Day);
        }

        [Fact]
        public void ShouldRegisterTick()
        {
            var timeSeries = new Mock<TimeSeries>();
            timeSeries.Setup(t => t.RegisterTick());
            userEvent.Type = "Tick";
            userEvent.Register(timeSeries.Object);
            timeSeries.VerifyAll();
        }

        [Fact]
        public void ShouldRegisterStart()
        {
            var timeSeries = new Mock<TimeSeries>();
            timeSeries.Setup(t => t.RegisterStart(userEvent.At));
            userEvent.Type = "Start";
            userEvent.Register(timeSeries.Object);
            timeSeries.VerifyAll();
        }

        [Fact]
        public void ShouldRegisterStop()
        {
            var timeSeries = new Mock<TimeSeries>();
            timeSeries.Setup(t => t.RegisterStop(userEvent.At));
            userEvent.Type = "Stop";
            userEvent.Register(timeSeries.Object);
            timeSeries.VerifyAll();
        }
    }
}