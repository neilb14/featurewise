using System;
using GF.FeatureWise.Client;
using Moq;
using Xunit;

namespace Tests.Client
{
    public class FeatureWiseScopeTest
    {
        [Fact]
        public void ShouldCommitOnDispose()
        {
            var fw = new Mock<IFeatureWise>();
            using(new FeatureWiseScope("Moose",DateTime.UtcNow, fw.Object)){}
            fw.Verify(f=>f.CommitScope("Moose", It.IsAny<DateTime>(), It.IsAny<DateTime>()));
        }
    }
}