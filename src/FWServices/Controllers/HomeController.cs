using System.Collections.Generic;
using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITimeSeriesRepository repository;
        private readonly IFeatureRepository featureRepository;

        public HomeController(ITimeSeriesRepository repository, IFeatureRepository featureRepository)
        {
            this.repository = repository;
            this.featureRepository = featureRepository;
        }

        public HomeController() : this(new TimeSeriesRepository(new ApiDataContext()), new FeatureRepository(new ApiDataContext()))
        {
        }

        public ActionResult Index()
        {
            ViewBag.Title = "FeatureWise Home";
            var results = repository.GetAll();
            var map = new Dictionary<string, string>();
            foreach (var result in results)
            {
                string data;
                if (map.TryGetValue(result.Feature, out data))
                {
                    map[result.Feature] = string.Format("{0},{1}", data, result.Ticks + result.Starts);
                }
                else
                {
                    map.Add(result.Feature, (result.Ticks + result.Starts).ToString());                    
                }                
            }
            ViewBag.Features = map;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
