using System;
using System.Web;
using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;
using GF.FeatureWise.Services.Services;

namespace GF.FeatureWise.Services.Controllers
{
    public class WorkbenchController : Controller
    {
        private readonly IUserEventRepository repository;        
        private readonly IParseUserEvents userEventParser;

        public WorkbenchController() :this(new UserEventRepository(new ApiDataContext()), new UserEventCsvParser())
        {
        }

        public WorkbenchController(IUserEventRepository repository, IParseUserEvents userEventParser)
        {
            this.repository = repository;
            this.userEventParser = userEventParser;
        }

        public ActionResult Index()
        {
            ViewBag.Events = repository.GetAll();
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string name, string type)
        {
            if (file == null || file.ContentLength <= 0)
            {
                repository.Add(new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Feature = name,
                    Type = type,
                    At = DateTime.UtcNow
                });
            }
            else
            {                
                foreach (var userEventRecord in userEventParser.FromStream(file.InputStream))
                {
                    repository.Add(userEventRecord);
                }
            }
            return RedirectToAction("Index", "Workbench");
        }
    }
}