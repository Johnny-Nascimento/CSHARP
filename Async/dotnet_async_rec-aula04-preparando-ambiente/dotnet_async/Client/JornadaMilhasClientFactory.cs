using System.Net.Http;

namespace dotnet_async.Client
{
    public class JornadaMilhasClientFactory : IHttpClientFactory
    {
        private string url = "http://localhost:5125";

        public HttpClient CreateClient(string name)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json")
            );

            return client;
        }
    }
}
