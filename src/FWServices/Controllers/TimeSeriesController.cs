using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class TimeSeriesController : Controller
    {
        private readonly ITimeSeriesRepository repository;
        private readonly IUserEventRepository userEventRepository;
        private readonly IGenerate<TimeSeries> generateTimeSeries;

        public TimeSeriesController(ITimeSeriesRepository repository, IUserEventRepository userEventRepository, IGenerate<TimeSeries> generateTimeSeries)
        {
            this.repository = repository;
            this.userEventRepository = userEventRepository;
            this.generateTimeSeries = generateTimeSeries;
        }

        public TimeSeriesController():this(new ApiDataContext())
        {
        }

        public TimeSeriesController(ApiDataContext context)
            : this(new TimeSeriesRepository(context), new UserEventRepository(context), new GenerateTimeSeries())
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
            foreach (var timeSeries in generateTimeSeries.Generate(userEventRepository.GetAll()))
            {
                repository.Add(timeSeries);
            }                        
            return new RedirectResult("/TimeSeries");
        }
    }
}