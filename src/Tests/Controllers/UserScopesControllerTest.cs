using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class UserScopesControllerTest
    {
        private readonly UserScopesController controller;
        private readonly Mock<IUserEventRepository> repository;
        private readonly UserScope userScope;

        public UserScopesControllerTest()
        {
            var request = new HttpRequestMessage();
            var config = new HttpConfiguration();
            userScope = new UserScope
            {
                Feature = "Moose",
                Id = Guid.NewGuid(),
                Type = "Tick",
                Start = DateTime.Now,
                Stop = DateTime.Now
            };
            config.Routes.Add("DefaultApi", new HttpRoute("api/{controller}/{id}"));
            request.Properties["MS_HttpConfiguration"] = config;
            repository = new Mock<IUserEventRepository>(MockBehavior.Strict);
            controller = new UserScopesController(repository.Object) { Request = request };
        }

        [Fact]
        public void ShouldCreateStartAndStopEvents()
        {
            repository.Setup(r => r.Add(It.IsAny<UserEvent>())).Returns(new UserEvent());            
            var result = controller.PostUserScope(userScope);
            repository.Verify(r=>r.Add(It.IsAny<UserEvent>()), Times.Exactly(2));
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

    }
}