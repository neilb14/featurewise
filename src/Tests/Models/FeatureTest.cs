using System.Collections.Generic;
using System.Linq;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Models
{
    public class FeatureTest
    {
        [Fact]
        public void Generate_ShouldCreateFeaturesFromUserEvents()
        {
            var repository = new Mock<IFeatureRepository>();
            var userEvents = new List<UserEvent>{new UserEvent{Feature="moose"}};

        }
    }
}