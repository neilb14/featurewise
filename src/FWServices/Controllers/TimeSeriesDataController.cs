using System.Collections.Generic;
using System.Web.Http;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class TimeSeriesDataController : ApiController
    {
        private readonly ITimeSeriesRepository timeSeriesRepository;        

        public TimeSeriesDataController()
            : this(new TimeSeriesRepository(new ApiDataContext()))
        {
        }        
        
        public TimeSeriesDataController(ITimeSeriesRepository timeSeriesRepository)
        {
            this.timeSeriesRepository = timeSeriesRepository;
        }

        public IEnumerable<TimeSeries> GetChartData()
        {            
            return timeSeriesRepository.GetAll();
        }       
    }
}
