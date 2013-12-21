using GF.FeatureWise.Services.Models;
using Xunit;

namespace Tests.Models
{
    public class SparklineBuilderTest
    {
        private readonly SparklineBuilder builder;

        public SparklineBuilderTest()
        {
            builder = new SparklineBuilder();
        }

        [Fact]
        public void ShouldBuildSparklineWithSingleDataPoint()
        {
            builder.Add(9);
            Assert.Equal("9", builder.Build(1));
        }

        [Fact]
        public void ShouldBuildSparklineWithMultipleDataPoints()
        {
            builder.Add(9);
            builder.Add(4);
            builder.Add(2);
            Assert.Equal("9,4,2", builder.Build(3));
        }

        [Fact]
        public void ShouldBuildSparklineWithMoreDataPointsThanNecessary()
        {
            builder.Add(9);
            builder.Add(4);
            builder.Add(2);
            Assert.Equal("4,2", builder.Build(2));
        }

        [Fact]
        public void ShouldBuildSparklineWithFewerDataPointsThanRequested()
        {
            builder.Add(9);
            builder.Add(4);
            Assert.Equal("9,4", builder.Build(3));
        }
    }
}
