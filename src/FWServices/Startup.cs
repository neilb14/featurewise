using System.Configuration;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (GF.FeatureWise.Services.Startup))]

namespace GF.FeatureWise.Services
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["ApiDataContext"].ConnectionString);
                config.UseServer();
            });
        }
    }
}