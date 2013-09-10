using System.Collections.Generic;
using System.Web.Http;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services.Controllers
{
    public class HistogramDataController : ApiController
    {
        private readonly IHistogramRepository histogramRepository;

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