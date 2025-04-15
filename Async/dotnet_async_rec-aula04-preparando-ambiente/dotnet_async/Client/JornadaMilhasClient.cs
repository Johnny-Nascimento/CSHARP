using dotnet_async.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_async.Client
{
    public class JornadaMilhasClient
    {
        private readonly HttpClient client;

        public JornadaMilhasClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Voo>> ConsultarVoosAsync(CancellationToken token = default)
        {
            // HttpResponseMessage response = await this.client.GetAsync("/Voos");
            // 
            // return await response.Content.ReadFromJsonAsync<IEnumerable<Voo>>();

            var response = await this.client.GetAsync("/Voos", token);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<Voo>>(token) ?? Enumerable.Empty<Voo>();
        }

        public async Task<string> ComprarPassagemAsync(CompraPassagemRequest request)
        {
            // return await this.client.PostAsJsonAsync("/Voos/Comprar", request).Result.Content.ReadFromJsonAsync<string>() ?? string.Empty;

            var response = await this.client.PostAsJsonAsync("/Voos/Comprar", request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<string>() ?? string.Empty;
        }
    }
}
