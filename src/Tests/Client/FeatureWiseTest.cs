using System;
using System.Net;
using GF.FeatureWise.Client;
using Xunit;

namespace Tests.Client
{    
    public class FeatureWiseTest
    {
        [Fact]
        public async void ShouldSendTickEvent()
        {
            Assert.Equal(HttpStatusCode.OK, await  FeatureWise.Tick("feature", "tick", DateTime.Now));
        }
    }
}