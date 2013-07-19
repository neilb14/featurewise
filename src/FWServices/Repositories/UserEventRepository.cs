using System;
using System.Collections.Generic;
using System.Linq;
using GF.FeatureWise.Services.Models;

namespace GF.FeatureWise.Services.Repositories
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