﻿using System.Linq;
using System.Web.Mvc;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;
using Hangfire;

namespace GF.FeatureWise.Services.Controllers
{
    public class HistogramController : Controller
    {
        private readonly IHistogramRepository repository;
        private readonly IUserEventRepository userEventRepository;
        private readonly IFeatureRepository featureRepository;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IGenerate<Histogram> generateHistogram;

        public HistogramController(IHistogramRepository repository, IUserEventRepository userEventRepository, IGenerate<Histogram> generateHistogram, IFeatureRepository featureRepository, IBackgroundJobClient backgroundJobClient)
        {
            this.repository = repository;
            this.userEventRepository = userEventRepository;
            this.generateHistogram = generateHistogram;
            this.featureRepository = featureRepository;
            this.backgroundJobClient = backgroundJobClient;
        }

        public HistogramController(ApiDataContext context, IFeatureRepository featureRepository, IBackgroundJobClient backgroundJobClient): this(new HistogramRepository(context), new UserEventRepository(context), new GenerateFeatureDecorator<Histogram>(new GenerateHistogram(), featureRepository), featureRepository, backgroundJobClient)
        {            
        }

        public HistogramController(ApiDataContext context) : this(context, new FeatureRepository(context), new BackgroundJobClient())
        {
        }

        public HistogramController():this(new ApiDataContext())
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
            backgroundJobClient.Enqueue(() => GenerateReport());
            return RedirectToAction("Index", "Histogram");
        }

        public void GenerateReport()
        {
            repository.DeleteAll();
            foreach (var histogram in generateHistogram.Generate(userEventRepository.GetAll()))
            {
                repository.Add(histogram);
            }
        }
    }
}
