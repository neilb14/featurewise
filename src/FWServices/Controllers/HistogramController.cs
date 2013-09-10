using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class HistogramController : Controller
    {
        private readonly IHistogramRepository repository;
        private readonly IUserEventRepository userEventRepository;

        public HistogramController(IHistogramRepository repository, IUserEventRepository userEventRepository)
        {
            this.repository = repository;
            this.userEventRepository = userEventRepository;
        }

        public HistogramController(ApiDataContext context): this(new HistogramRepository(context), new UserEventRepository(context))
        {            
        }

        public HistogramController():this(new ApiDataContext())
        {
        }

        public ActionResult Index(string accessor = "TotalUsage")
        {
            ViewBag.Accessor = accessor;
            return View();
        }


        [HttpPost]
        public ActionResult Generate()
        {
            repository.DeleteAll();
            foreach (var histogram in Histogram.Generate(userEventRepository.GetAll()))
            {
                repository.Add(histogram);
            }
            return new RedirectResult("/Histogram");
        }

    }
}
