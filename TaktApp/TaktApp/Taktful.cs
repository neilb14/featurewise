using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaktApp
{
    public class Taktful
    {
        public static async Task<HttpStatusCode> Tick(string project, string feature, string type, DateTime timestamp)
        {
            var content = string.Format("{{\"project\":\"{0}\",\"name\":\"{1}\",\"type\":\"{2}\",\"at\":\"{3}\"}}", project, feature, type, timestamp.ToString("yyyyMMddHHmmss"));            
            var url = String.Format("http://taktapp.herokuapp.com/api/{0}/events", project);
            var result = await new HttpClient().PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));            
            return result.StatusCode;
        }
    }
}