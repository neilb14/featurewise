using System;
using GF.FeatureWise.Client;
using Xunit;

namespace Tests.Client
{    
    public class JsonContentTest
    {
        [Fact]
        public void ShouldGenerateJsonForPosting()
        {
            var at = new DateTime(2013,9,5,8,0,30);
            Assert.Equal("{\"Id\":\"10fb9c71-a314-41ea-9dca-369824d14dad\",\"Feature\":\"moose\",\"Type\":\"tick\",\"At\":\"20130905080030\"}",JsonContent.Build(Guid.Parse("10fb9c71-a314-41ea-9dca-369824d14dad"), "moose", "tick", at));
        }        
    }
}