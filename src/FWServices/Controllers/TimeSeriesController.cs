using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;
using Hangfire;

namespace GF.FeatureWise.Services.Controllers
{
    public class TimeSeriesController : Controller
    {
        private const int SPARKLINE_LENGTH_IN_DAYS = 30;
        private readonly ApiDataContext context;
        private readonly ITimeSeriesRepository timeSeriesRepository;
        private readonly IFeatureRepository featureRepository;
        private readonly IUserEventRepository userEventRepository;
        private readonly IGenerate<TimeSeries> generateTimeSeries;

        public TimeSeriesController(ApiDataContext context, ITimeSeriesRepository timeSeriesRepository, IUserEventRepository userEventRepository, IGenerate<TimeSeries> generateTimeSeries, IFeatureRepository featureRepository)
        {
            this.context = context;
            this.timeSeriesRepository = timeSeriesRepository;
            this.userEventRepository = userEventRepository;
            this.generateTimeSeries = generateTimeSeries;
            this.featureRepository = featureRepository;
        }

        public TimeSeriesController():this(new ApiDataContext())
        {
        }

        public TimeSeriesController(ApiDataContext context): this(context, new FeatureRepository(context))
        {
        }

        public TimeSeriesController(ApiDataContext context, IFeatureRepository featureRepository)
            : this(context, new TimeSeriesRepository(context), new UserEventRepository(context), new GenerateFeatureDecorator<TimeSeries>(new GenerateTimeSeries(),featureRepository), featureRepository)
        {
        }

        public ActionResult Index(string accessor = "TotalUsage")
        {
            var features = featureRepository.GetAll().ToArray();            
            var groups = features.Select(feature => feature.Group).Distinct().ToList();
            ViewBag.Groups = groups;
            ViewBag.Features = features;
            ViewBag.Accessor = accessor;
            return View();
        }

        [HttpPost]
        public ActionResult Generate()
        {
            BackgroundJob.Enqueue(() => GenerateReports());
            return RedirectToAction("Index", "TimeSeries");
        }

        public void GenerateReports()
        {
            timeSeriesRepository.DeleteAll();
            var map = new Dictionary<string, IBuildSparkline>();
            foreach (var timeSeries in generateTimeSeries.Generate(userEventRepository.GetAll()))
            {
                timeSeriesRepository.Add(timeSeries);
                IBuildSparkline builder;
                if (!map.TryGetValue(timeSeries.Feature, out builder))
                    map.Add(timeSeries.Feature, builder = new SparklineBuilder());
                builder.Add(timeSeries.Ticks + timeSeries.Starts);
            }
            foreach (var name in map.Keys)
            {
                var feature = featureRepository.Get(name);
                feature.Sparkline = map[name].Build(SPARKLINE_LENGTH_IN_DAYS);
            }
            context.SaveChanges();
        }
    }
}