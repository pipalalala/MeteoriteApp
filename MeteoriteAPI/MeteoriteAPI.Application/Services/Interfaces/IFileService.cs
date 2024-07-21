using MeteoriteAPI.Application.Models.Clients.Nasa;

namespace MeteoriteAPI.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task CreateFileAsync(IEnumerable<Meteorite> meteorites);

        Task<IEnumerable<string>> GetFilesAsync();

        Task<IEnumerable<Meteorite>> GetDataAsync(string filePath);

        Task DeletefileAsync(string filePath);
    }
}
