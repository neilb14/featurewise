using System;

namespace FWServices.Models
{
    public class UserEvent
    {
        public Guid Id { get; set; }
        public string Feature { get; set; }
        public string Type { get; set; }
        public DateTime At { get; set; }
    }
}