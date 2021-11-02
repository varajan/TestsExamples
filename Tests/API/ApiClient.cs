using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Tests.Data;

namespace Tests.API
{
    public static class ApiClient
    {
        private static HttpClient Client { get; }

        static ApiClient() => Client = new HttpClient();

        public static void Post(string url, object model = null) => SendRequest(HttpMethod.Post, url, model);
        public static void Delete(string url, object model = null) => SendRequest(HttpMethod.Delete, url, model);

        private static void SendRequest(HttpMethod method, string url, object model = null)
        {
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = model == null
                ? new HttpRequestMessage(method, $"{Defaults.BaseUrl}/{url}")
                : new HttpRequestMessage(method, $"{Defaults.BaseUrl}/{url}") { Content = content };

            //var rr = Client.SendAsync(request).Result;
            Client.SendAsync(request).Wait();
        }
    }
}
