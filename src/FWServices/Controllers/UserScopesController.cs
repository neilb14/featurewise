using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GF.FeatureWise.Services.Models;
using GF.FeatureWise.Services.Repositories;

namespace GF.FeatureWise.Services.Controllers
{
    public class UserScopesController : ApiController
    {
        private readonly IUserEventRepository repository;

        public UserScopesController() : this(new UserEventRepository(new ApiDataContext()))
        {
        }

        public UserScopesController(IUserEventRepository repository)
        {
            this.repository = repository;
        }

     
        public HttpResponseMessage PostUserScope(UserScope userScope)
        {
            repository.Add(userScope.StartEvent());
            var result = repository.Add(userScope.StopEvent());
            var response = Request.CreateResponse(HttpStatusCode.Created, result);
            var uri = Url.Link("DefaultApi", new { controller = "UserScopes", id = result.Id });
            if (!String.IsNullOrEmpty(uri))
            {
                response.Headers.Location = new Uri(uri);
            }
            return response;
        }
    }
}