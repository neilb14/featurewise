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
        public void GetIndex_ShouldBuildListOfFeaturesWithSparklines()
        {
            featureRepository.Setup(r => r.GetAll()).Returns(new[]
                {
                    new Feature {Sparkline="40,46,42"},
                    new Feature {},
                    new Feature {}
                });
            controller.Index();
            var features = controller.ViewBag.Features;            
            Assert.Equal(4, features.Count);         
            Assert.Equal("40,46,42", features[0].Sparkline);
            featureRepository.VerifyAll();
        }
    }
}