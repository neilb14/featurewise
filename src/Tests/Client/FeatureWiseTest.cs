using System;
using System.Net;
using GF.FeatureWise.Client;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FeatureWiseTest
    {
        [Test]
        public async void ShouldSendTickEvent()
        {
            Assert.AreEqual(HttpStatusCode.OK, await  FeatureWise.Tick("feature", "tick", DateTime.Now));
        }
    }
}