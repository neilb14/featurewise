using System.Data.Entity;
using FWServices.Models;

namespace FWServices.DataContext
{
    public class ApiDataContext : DbContext
    {
        public DbSet<UserEvent> UserEvents { get; set; }
    }
}