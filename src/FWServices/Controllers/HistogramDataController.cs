using System.Collections.Generic;
using System.Web.Http;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class HistogramDataController : ApiController
    {
        private readonly IHistogramRepository histogramRepository;

        public HistogramDataController() : this(new HistogramRepository(new ApiDataContext()))
        {
        }

        public HistogramDataController(IHistogramRepository histogramRepository)
        {
            this.histogramRepository = histogramRepository;
        }

        public IEnumerable<Histogram> GetData()
        {
            return histogramRepository.GetAll();
        }
    }
}