using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaktApp
{
    public class Taktful
    {
        public static async Task<HttpStatusCode> Tick(string project, string feature, string type, DateTime timestamp)
        {
            var data = new Dictionary<string, string>
                {
                    {"project", project},
                    {"name", feature},
                    {"type", type},
                    {"at", timestamp.ToString("yyyyMMddHHmmss")},
                };
            var content = await JsonConvert.SerializeObjectAsync(data);
            var url = String.Format("http://taktapp.herokuapp.com/api/{0}/events", project);
            var result = await new HttpClient().PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));            
            return result.StatusCode;
        }
    }
}