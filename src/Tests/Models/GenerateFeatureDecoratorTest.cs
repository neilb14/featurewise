using System.Collections.Generic;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Models
{
    public class GenerateFeatureDecoratorTest
    {
        [Fact]
        public void Generate_ShouldNotCreateFeaturesFromUserEventsWhenFeatureAlreadyExists()
        {
            var realGenerator = new Mock<IGenerate<object>>();
            var repository = new Mock<IFeatureRepository>();
            var userEvents = new List<UserEvent>{new UserEvent{Feature="moose"}};
            var decorator = new GenerateFeatureDecorator<object>(realGenerator.Object, repository.Object);
            var expectedResults = new[] {new object()};
            realGenerator.Setup(g => g.Generate(userEvents)).Returns(expectedResults);
            repository.Setup(r => r.Exists("moose")).Returns(true);
            var results = decorator.Generate(userEvents);
            realGenerator.VerifyAll();
            repository.VerifyAll();
            Assert.Same(expectedResults, results);
        }

        [Fact]
        public void Generate_ShouldCreateFeaturesFromUserEventsWhenFeatureDoesNotExist()
        {
            var realGenerator = new Mock<IGenerate<object>>();
            var repository = new Mock<IFeatureRepository>();
            var userEvents = new List<UserEvent> { new UserEvent { Feature = "moose" } };
            var decorator = new GenerateFeatureDecorator<object>(realGenerator.Object, repository.Object);
            var expectedResults = new[] { new object() };
            realGenerator.Setup(g => g.Generate(userEvents)).Returns(expectedResults);
            repository.Setup(r => r.Exists("moose")).Returns(false);
            repository.Setup(r => r.Add(It.IsAny<Feature>())).Returns(new Feature());
            var results = decorator.Generate(userEvents);
            realGenerator.VerifyAll();
            repository.VerifyAll();
            Assert.Same(expectedResults, results);
        }
    }
}