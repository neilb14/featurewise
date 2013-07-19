using System;
using System.Collections.Generic;

namespace GF.FeatureWise.Services.Models
{
    public interface IUserEventRepository
    {
        UserEvent Add(UserEvent userEvent);
        IEnumerable<UserEvent> GetAll();
        UserEvent Get(Guid id);
    }
}