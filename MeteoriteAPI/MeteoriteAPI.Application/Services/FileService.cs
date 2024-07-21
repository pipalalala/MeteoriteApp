using Newtonsoft.Json;
using MeteoriteAPI.Application.Services.Interfaces;
using MeteoriteAPI.Application.Models.Clients.Nasa;

namespace MeteoriteAPI.Application.Services
{
    public class FileService : IFileService
    {
        private const string FilePrefix = "MeteoriteAPI_File";
        private const string FilesDirectory = "MeteoriteAPI_Files";

        private readonly string FilesDirectoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            FilesDirectory);

        public FileService()
        {
            Directory.CreateDirectory(FilesDirectoryPath);
        }

        public async Task CreateFileAsync(IEnumerable<Meteorite> meteorites)
        {
            if (meteorites is null)
            {
                throw new ArgumentNullException(nameof(meteorites));
            }

            var guid = Guid.NewGuid();
            var fileName = $"{FilePrefix}_{guid}_{DateTime.Now:dd-MM-yyyy_hh-mm-ss-fff}.txt";
            var filePath = Path.Combine(FilesDirectoryPath, fileName);

            using (var stream = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();

                serializer.Serialize(stream, meteorites);

                await stream.FlushAsync().ConfigureAwait(false);
            }
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            IEnumerable<string> files = Directory.GetFiles(FilesDirectoryPath);

            return Task.FromResult(files);
        }

        public Task<IEnumerable<Meteorite>> GetDataAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using var streamReader = File.OpenText(filePath);
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();

                var result = serializer.Deserialize<IEnumerable<Meteorite>>(jsonReader);

                return Task.FromResult(result!);
            }
        }

        public Task DeletefileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            return Task.Run(() => File.Delete(filePath));
        }
    }
}
