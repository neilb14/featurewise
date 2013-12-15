using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using GF.FeatureWise.Services;
using GF.FeatureWise.Services.Controllers;
using GF.FeatureWise.Services.Models;
using Moq;
using Xunit;

namespace Tests.Controllers
{
    public class FeatureControllerTest
    {
        private readonly Mock<IFeatureRepository> repository;
        private readonly FeatureController controller;
        private readonly Mock<ApiDataContext> context;

        public FeatureControllerTest()
        {
            var request = new HttpRequestMessage();
            var config = new HttpConfiguration();
            config.Routes.Add("DefaultApi", new HttpRoute("api/{controller}/{id}"));
            request.Properties["MS_HttpConfiguration"] = config;
            context = new Mock<ApiDataContext>(); 
            repository = new Mock<IFeatureRepository>(MockBehavior.Strict);
            controller = new FeatureController(context.Object, repository.Object) { Request = request };
        }

        [Fact]
        public void PostFeature_ShouldSaveChangesToFeature()
        {            
            var existing = new Feature();
            repository.Setup(r => r.Get("moose")).Returns(existing);
            HttpResponseMessage response =
                controller.PutUserEvent(new Feature
                    {
                        Name = "moose",
                        Group = "Canada",
                        Notes = "These features are big Canadian animals."
                    });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Canada", existing.Group);
            Assert.Equal("These features are big Canadian animals.", existing.Notes);
            context.Verify(c => c.SaveChanges());
        }
    }
}