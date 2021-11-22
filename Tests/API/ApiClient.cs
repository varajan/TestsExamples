using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Tests.Data;

namespace Tests.API
{
    public static class ApiClient
    {
        private static HttpClient Client { get; }

        static ApiClient()
        {

            Client = new HttpClient();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public static HttpResponseMessage Post(string url, object model = null) => SendRequest(HttpMethod.Post, url, model);
        public static HttpResponseMessage Delete(string url, object model = null) => SendRequest(HttpMethod.Delete, url, model);

        private static HttpResponseMessage SendRequest(HttpMethod method, string url, object model = null)
        {
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = model == null
                ? new HttpRequestMessage(method, $"{Defaults.BaseUrl}/api/{url}")
                : new HttpRequestMessage(method, $"{Defaults.BaseUrl}/api/{url}") { Content = content };

            return Client.SendAsync(request).Result;
        }
    }
}
