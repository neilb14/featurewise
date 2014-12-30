using System.IO;
using System.Web;
using System.Web.Mvc;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class WorkbenchControllerTest
    {
        private readonly WorkbenchController controller;
        private readonly Mock<IUserEventRepository> repository;
        private readonly Mock<IParseUserEvents> parser;
        private readonly Mock<HttpPostedFileBase> file;

        public WorkbenchControllerTest()
        {
            repository = new Mock<IUserEventRepository>(MockBehavior.Strict);
            parser = new Mock<IParseUserEvents>();
            file = new Mock<HttpPostedFileBase>();
            controller = new WorkbenchController(repository.Object, null);
        }

        [Fact]
        public void GetIndex_ShouldGetAllUserEvents()
        {
            repository.Setup(r => r.GetAll()).Returns(new[] {new UserEvent(), new UserEvent(), new UserEvent()});
            var response = controller.Index();
            repository.VerifyAll();
            Assert.NotNull(response);
        }

        [Fact]
        public void PostIndex_ShouldSaveASingleEvent_WhenFileIsNull()
        {
            repository.Setup(r => r.Add(It.IsAny<UserEvent>())).Returns(new UserEvent());
            var result = controller.Index(null, "Moose", "Tick") as RedirectToRouteResult;
            repository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("Workbench", result.RouteValues["controller"]);
            Assert.Equal("Index", result.RouteValues["action"]);
        }        

        [Fact]
        public void PostIndex_ShouldSaveMultipleUserEvents_WhenFileIsUploaded()
        {
            parser.Setup(p => p.FromStream(It.IsAny<Stream>()))
                  .Returns(new[] {new UserEvent(), new UserEvent(), new UserEvent(),});
            repository.Setup(r => r.Add(It.IsAny<UserEvent>())).Returns(new UserEvent());
            var result = controller.Index(file.Object, "Moose", "Tick") as RedirectToRouteResult;
            repository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("Workbench", result.RouteValues["controller"]);
            Assert.Equal("Index", result.RouteValues["action"]);
        }
    }
}