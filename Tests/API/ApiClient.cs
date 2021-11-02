using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Tests.API
{
    public static class ApiClient
    {
        public static void Post(string url, object model = null)
        {
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = model == null
                ? new HttpRequestMessage(HttpMethod.Post, url)
                : new HttpRequestMessage(HttpMethod.Post, url) { Content = content };

            new HttpClient().SendAsync(request).Wait();
        }
    }
}
