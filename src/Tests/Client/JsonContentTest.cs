using System;
using GF.FeatureWise.Client;
using Xunit;

namespace Tests.Client
{    
    public class JsonContentTest
    {
        [Fact]
        public void ShouldGenerateJsonForPostingAUserEvent()
        {
            var at = new DateTime(2013,9,5,8,15,30);
            Assert.Equal("{\"Id\":\"10fb9c71-a314-41ea-9dca-369824d14dad\",\"Feature\":\"moose\",\"Type\":\"tick\",\"At\":\"2013-09-05T08:15:30.0000000\"}",JsonContent.BuildEvent(Guid.Parse("10fb9c71-a314-41ea-9dca-369824d14dad"), "moose", "tick", at));
        }

        [Fact]
        public void ShouldGenerateJsonForPostingAUserScope()
        {
            var start = new DateTime(2013, 9, 5, 8, 15, 30);
            var stop = new DateTime(2013, 9, 5, 8, 19, 59);
            Assert.Equal("{\"Id\":\"10fb9c71-a314-41ea-9dca-369824d14dad\",\"Feature\":\"moose\",\"Type\":\"scope\",\"Start\":\"2013-09-05T08:15:30.0000000\",\"Stop\":\"2013-09-05T08:19:59.0000000\"}", JsonContent.BuildScope(Guid.Parse("10fb9c71-a314-41ea-9dca-369824d14dad"), "moose", "scope", start, stop));
        }
    }
}