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
        private Mock<IUserEventRepository> repository;

        public WorkbenchControllerTest()
        {
            repository = new Mock<IUserEventRepository>();
            controller = new WorkbenchController(repository.Object);
        }

        [Fact]
        public void ShouldPostASingleEventToWorkbench()
        {
            repository.Setup(r => r.Add(It.IsAny<UserEvent>())).Returns(new UserEvent());
            var result = controller.Index("Moose", "Tick") as RedirectResult;
            repository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal("/Workbench", result.Url);
        }
    }
}