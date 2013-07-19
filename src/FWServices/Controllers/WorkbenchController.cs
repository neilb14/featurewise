using System;
using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class WorkbenchController : Controller
    {
        private readonly IUserEventRepository repository;

        public WorkbenchController() :this(new UserEventRepository(new ApiDataContext()))
        {
        }

        public WorkbenchController(IUserEventRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, string type)
        {
            repository.Add(new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Feature = name,
                    Type = type,
                    At = DateTime.UtcNow
                });
            return new RedirectResult("/Workbench");
        }
    }
}