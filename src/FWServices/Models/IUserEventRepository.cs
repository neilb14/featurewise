using System;
using System.Collections.Generic;

namespace FWServices.Models
{
    public interface IUserEventRepository
    {
        UserEvent Add(UserEvent userEvent);
        IEnumerable<UserEvent> GetAll();
        UserEvent Get(Guid id);
    }
}