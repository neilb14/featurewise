using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class HistogramController : Controller
    {
        private readonly IHistogramRepository repository;
        private readonly IUserEventRepository userEventRepository;
        private readonly IGenerate<Histogram> generateHistogram;

        public HistogramController(IHistogramRepository repository, IUserEventRepository userEventRepository, IGenerate<Histogram> generateHistogram)
        {
            this.repository = repository;
            this.userEventRepository = userEventRepository;
            this.generateHistogram = generateHistogram;
        }

        public HistogramController(ApiDataContext context): this(new HistogramRepository(context), new UserEventRepository(context), new GenerateFeatureDecorator<Histogram>(new GenerateHistogram(), new FeatureRepository(context)))
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
            foreach (var histogram in generateHistogram.Generate(userEventRepository.GetAll()))
            {
                repository.Add(histogram);
            }
            return new RedirectResult("/Histogram");
        }

    }
}
