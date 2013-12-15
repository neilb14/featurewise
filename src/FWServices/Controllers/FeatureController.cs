using System.Net;
using System.Net.Http;
using System.Web.Http;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class FeatureController : ApiController
    {
        private readonly ApiDataContext context;
        private readonly IFeatureRepository repository;

        public FeatureController() :this(new ApiDataContext())
        {
        }

        public FeatureController(ApiDataContext context) :this(context, new FeatureRepository(context))
        {            
        }

        public FeatureController(ApiDataContext context, IFeatureRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        public HttpResponseMessage PutUserEvent(Feature feature)
        {
            var existing = repository.Get(feature.Name);
            existing.Group = feature.Group;
            existing.Notes = feature.Notes;
            context.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK);            
            return response;
        }
    }
}