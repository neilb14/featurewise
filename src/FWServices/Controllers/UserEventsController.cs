using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FWServices.Models;
using FWServices.Repositories;

namespace FWServices.Controllers
{
    public class UserEventsController : ApiController
    {
        private readonly IUserEventRepository repository;

        public UserEventsController() : this(new UserEventRepository(new ApiDataContext()))
        {
        }

        public UserEventsController(IUserEventRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<UserEvent> GetAllUserEvents()
        {
            return repository.GetAll();
        }

        public UserEvent GetUserEvent(Guid id)
        {
            return repository.Get(id);
        }

        public HttpResponseMessage PostUserEvent(UserEvent userEvent)
        {
            var result = repository.Add(userEvent);
            var response = Request.CreateResponse(HttpStatusCode.Created, result);
            var uri = Url.Link("DefaultApi", new {controller = "UserEvents", id = result.Id});            
            if (!String.IsNullOrEmpty(uri))
            {
                response.Headers.Location = new Uri(uri);   
            }            
            return response;
        }
    }    
}
