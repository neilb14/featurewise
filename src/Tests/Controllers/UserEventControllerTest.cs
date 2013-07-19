using System;
using System.Linq;
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
    public class UserEventControllerTest
    {
        private readonly UserEventsController controller;
        private readonly Mock<IUserEventRepository> repository;
        private readonly UserEvent userEvent;

        public UserEventControllerTest()
        {
            var request = new HttpRequestMessage();
            var config = new HttpConfiguration();
            userEvent = new UserEvent
                {
                    Feature = "Moose",
                    Id = Guid.NewGuid(),
                    Type = "Tick",
                    At = DateTime.Now
                };
            config.Routes.Add("DefaultApi", new HttpRoute("api/{controller}/{id}"));
            request.Properties["MS_HttpConfiguration"] = config;
            repository = new Mock<IUserEventRepository>(MockBehavior.Strict);
            controller = new UserEventsController(repository.Object) { Request = request };
        }

        [Fact]
        public void ShouldGetASingleUserEvent()
        {
            repository.Setup(r => r.Get(Guid.Empty)).Returns(userEvent);
            var result = controller.GetUserEvent(Guid.Empty);
            repository.VerifyAll();
            Assert.Equal(userEvent, result);
        }

        [Fact]
        public void ShouldGetAllUserEvents()
        {
            repository.Setup(r => r.GetAll()).Returns(new[] {userEvent, userEvent, userEvent});
            var result = controller.GetAllUserEvents();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ShouldCreateNewUserEvent()
        {
            repository.Setup(r => r.Add(userEvent)).Returns(userEvent);
            var result = controller.PostUserEvent(userEvent);
            repository.VerifyAll();
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }        
    }
}