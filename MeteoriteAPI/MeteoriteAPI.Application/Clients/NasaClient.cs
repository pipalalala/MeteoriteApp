using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MeteoriteAPI.Infrastructure.Models.Options;
using MeteoriteAPI.Application.Clients.Interfaces;
using MeteoriteAPI.Application.Models.Clients.Nasa;

namespace MeteoriteAPI.Application.Clients
{
    public class NasaClient(
        HttpClient _httpClient,
        IOptions<NasaClientOptions> _nasaClientOptions) : INasaClient
    {
        public async Task<IEnumerable<Meteorite>?> GetMeteoritesInfoAsync()
        {
            var result = await _httpClient.GetAsync(_nasaClientOptions.Value.MeteoriteUrl);

            if (result.IsSuccessStatusCode)
            {
                var resultString = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Meteorite>>(resultString);
            }

            return null;
        }
    }
}
