using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaktApp
{
    public class Taktful
    {
        public static async Task<HttpStatusCode> Tick(string feature, string type, DateTime timestamp)
        {
            var hostname = ConfigurationManager.AppSettings["TaktApp.Hostname"];
            var project = ConfigurationManager.AppSettings["TaktApp.Project"];            
            var url = String.Format("{0}/api/{1}/events", hostname, project);
            var result = await new HttpClient().PostAsync(url, new StringContent(JsonContent.Build(project,feature,type,timestamp), Encoding.UTF8, "application/json"));
            return result.StatusCode;
        }
    }
}