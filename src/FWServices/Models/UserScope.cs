using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GF.FeatureWise.Services.Models
{
    public class UserScope
    {
        public Guid Id { get; set; }
        public string Feature { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public UserEvent StartEvent()
        {
            return new UserEvent {Id = Id, Type = "Start", Feature = Feature, At = Start};
        }

        public UserEvent StopEvent()
        {
            return new UserEvent { Id = Id, Type = "Stop", Feature = Feature, At = Stop };
        }
    }
}