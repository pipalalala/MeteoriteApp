using MeteoriteAPI.Application.Models.Clients.Nasa;

namespace MeteoriteAPI.Application.Clients.Interfaces
{
    public interface INasaClient
    {
        Task<IEnumerable<Meteorite>?> GetMeteoritesInfoAsync();
    }
}
