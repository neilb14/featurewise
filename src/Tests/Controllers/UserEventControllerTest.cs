using System;
using System.Net;
using FWServices.Controllers;
using FWServices.Models;
using Xunit;


namespace Tests.Controllers
{    
    public class UserEventControllerTest
    {
        [Fact]
        public void ShouldCreateNewUserEvent()
        {
            var controller = new UserEventsController();
            var userEvent = new UserEvent
                {
                    Feature = "Moose", Id = Guid.NewGuid(), Type = "Tick", At = DateTime.Now
                };
            var result = controller.PostUserEvent(userEvent);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
    }
}