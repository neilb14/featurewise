using System;
using System.Net;
using NUnit.Framework;
using TaktApp;

namespace Tests
{
    [TestFixture]
    public class TaktfulTest
    {
        [Test]
        public async void ShouldSendTickEvent()
        {
            Assert.AreEqual(HttpStatusCode.OK, await Taktful.Tick("feature", "tick", DateTime.Now));
        }
    }
}