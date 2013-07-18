using System;
using System.Collections.Generic;
using System.Linq;
using FWServices.DataContext;
using FWServices.Models;

namespace FWServices.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly ApiDataContext context;

        public UserEventRepository(ApiDataContext context)
        {
            this.context = context;
        }

        public UserEvent Add(UserEvent userEvent)
        {            
            var result = context.UserEvents.Add(userEvent);
            context.SaveChanges();
            return result;
        }

        public IEnumerable<UserEvent> GetAll()
        {
            return context.UserEvents.AsEnumerable();         
        }

        public UserEvent Get(Guid id)
        {         
            return context.UserEvents.Find(id);         
        }
    }
}