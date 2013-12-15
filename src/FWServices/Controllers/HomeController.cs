using System.Collections.Generic;
using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureRepository featureRepository;

        public HomeController(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
        }

        public HomeController() : this(new FeatureRepository(new ApiDataContext()))
        {
        }

        public ActionResult Index()
        {
            ViewBag.Title = "FeatureWise Home";            
            IDictionary<string, List<Feature>> map = new Dictionary<string, List<Feature>>();
            foreach (var feature in featureRepository.GetAll())
            {
                List<Feature> group;
                var groupName = feature.Group ?? "Ungrouped";
                if (!map.TryGetValue(groupName, out group))
                    map.Add(groupName, group = new List<Feature>());
                group.Add(feature);
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
