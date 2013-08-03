using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class ChartDataController : ApiController
    {
        private readonly ITimeSeriesRepository repository;

        public ChartDataController() :this(new TimeSeriesRepository(new ApiDataContext()))
        {
        }

        public ChartDataController(ITimeSeriesRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<TimeSeries> GetChartData()
        {            
            return repository.GetAll();
        }
    }
}
