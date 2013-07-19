using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class TimeSeriesController : Controller
    {
        private readonly ITimeSeriesRepository repository;
        private readonly IUserEventRepository userEventRepository;

        public TimeSeriesController(ITimeSeriesRepository repository, IUserEventRepository userEventRepository)
        {
            this.repository = repository;
            this.userEventRepository = userEventRepository;
        }

        public TimeSeriesController():this(new ApiDataContext())
        {
        }

        public TimeSeriesController(ApiDataContext context)
            : this(new TimeSeriesRepository(context), new UserEventRepository(context))
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Generate()
        {
            repository.DeleteAll();
            foreach (var timeSeries in TimeSeries.Generate(userEventRepository.GetAll()))
            {
                repository.Add(timeSeries);
            }                        
            return new RedirectResult("/TimeSeries");
        }
    }
}