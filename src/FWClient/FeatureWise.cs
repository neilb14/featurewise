using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GF.FeatureWise.Client
{
    public class FeatureWise
    {
        public static async Task<HttpStatusCode> Tick(string feature, string type, DateTime timestamp)
        {
            var hostname = ConfigurationManager.AppSettings["TaktApp.Hostname"];            
            var url = String.Format("{0}/api/UserEvents", hostname);
            var result = await new HttpClient().PostAsync(url, new StringContent(JsonContent.Build(Guid.NewGuid(),feature,type,timestamp), Encoding.UTF8, "application/json"));
            return result.StatusCode;
        }
    }
}