using System.Collections.Generic;
using System.Linq;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IFeatureRepository> featureRepository;
        private readonly HomeController controller;

        public HomeControllerTest()
        {
            featureRepository = new Mock<IFeatureRepository>();
            controller = new HomeController(featureRepository.Object);
        }

        [Fact]
        public void GetIndex_ShouldBuildGroupsOfFeatures()
        {
            featureRepository.Setup(r => r.GetAll()).Returns(new[]
                {
                    new Feature {Name= "Moose", Group = "Admin"},
                    new Feature {Name= "Rhino", Group = "User"},
                    new Feature {Name= "Cheetah", Group = "User"}
                });
            controller.Index();
            IDictionary<string, List<Feature>> features = controller.ViewBag.Features;            
            Assert.Equal(2, features.Keys.Count);
            Assert.True(features.ContainsKey("Admin"));
            Assert.True(features.ContainsKey("User"));
            var mooseFeature = features["Admin"].First();
            Assert.Equal("Moose", mooseFeature.Name);            
            featureRepository.VerifyAll();
        }

        [Fact]
        public void GetIndex_ShouldCollectUngroupedFeatures()
        {
            featureRepository.Setup(r => r.GetAll()).Returns(new[]
                {
                    new Feature {Name= "Moose"},
                    new Feature {Name= "Rhino"},
                    new Feature {Name= "Cheetah"}
                });
            controller.Index();
            IDictionary<string, List<Feature>> features = controller.ViewBag.Features;
            Assert.Equal(1, features.Keys.Count);
            Assert.True(features.ContainsKey("Ungrouped"));            
            Assert.Equal(3, features["Ungrouped"].Count());
        }
    }
}