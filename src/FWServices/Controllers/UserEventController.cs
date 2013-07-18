using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FWServices.Models;

namespace FWServices.Controllers
{
    public class UserEventController : ApiController
    {
        public HttpResponseMessage PostUserEvent(UserEvent userEvent)
        {            
            var response = Request.CreateResponse(HttpStatusCode.Created, userEvent);
            string uri = Url.Link("DefaultApi", new { id = userEvent.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
