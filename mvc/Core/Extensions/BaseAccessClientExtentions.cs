using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class BaseAccessClientExtentions
    {
        public static async Task<HttpResponseMessage> PostObjectAsync(this HttpClient httpClient, string url, object model)
        {
            return await httpClient.PostAsync(url,
                new StringContent(
                    JsonConvert.SerializeObject(
                        model,
                        Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        }),
                    Encoding.UTF8,
                    "application/json"));
        }
    }
}
