using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FWServices.Models;
using FWServices.DataContext;

namespace FWServices.Controllers
{
    public class Default1Controller : ApiController
    {
        private ApiDataContext db = new ApiDataContext();

        // GET api/Default1
        public IEnumerable<UserEvent> GetUserEvents()
        {
            return db.UserEvents.AsEnumerable();
        }

        // GET api/Default1/5
        public UserEvent GetUserEvent(Guid id)
        {
            UserEvent userevent = db.UserEvents.Find(id);
            if (userevent == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userevent;
        }

        // PUT api/Default1/5
        public HttpResponseMessage PutUserEvent(Guid id, UserEvent userevent)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != userevent.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(userevent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Default1
        public HttpResponseMessage PostUserEvent(UserEvent userevent)
        {
            if (ModelState.IsValid)
            {
                db.UserEvents.Add(userevent);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userevent);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userevent.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Default1/5
        public HttpResponseMessage DeleteUserEvent(Guid id)
        {
            UserEvent userevent = db.UserEvents.Find(id);
            if (userevent == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserEvents.Remove(userevent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userevent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}