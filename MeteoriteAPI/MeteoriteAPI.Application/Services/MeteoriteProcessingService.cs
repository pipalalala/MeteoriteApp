using AutoMapper;
using MeteoriteAPI.Domain.Repositories.Interfaces;
using MeteoriteAPI.Application.Clients.Interfaces;
using MeteoriteAPI.Application.Services.Interfaces;
using MeteoriteAPI.Domain.Models.EF;

namespace MeteoriteAPI.Application.Services
{
    public class MeteoriteProcessingService : IMeteoriteProcessingService
    {
        private const int BatchSize = 100;

        private readonly IMapper _mapper;
        private readonly INasaClient _nasaClient;
        private readonly IFileService _fileService;
        private readonly IMeteoriteRepository _meteoriteRepository;

        public MeteoriteProcessingService(
            IMapper mapper,
            INasaClient nasaClient,
            IFileService fileService,
            IMeteoriteRepository meteoriteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _nasaClient = nasaClient ?? throw new ArgumentNullException(nameof(nasaClient));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _meteoriteRepository = meteoriteRepository
                ?? throw new ArgumentNullException(nameof(meteoriteRepository));
        }

        public async Task PrepareDataAsync()
        {
            var meteorites = await _nasaClient.GetMeteoritesInfoAsync();

            if (meteorites == null) return;

            var batches = meteorites.Chunk(BatchSize);

            foreach (var batch in batches)
            {
                await _fileService.CreateFileAsync(batch);
            }
        }

        public async Task ProcessDataAsync()
        {
            var files = await _fileService.GetFilesAsync();

            foreach (var file in files)
            {
                var meteorites = await _fileService.GetDataAsync(file);

                var meteoritesDto = _mapper.Map<IEnumerable<Meteorite>>(meteorites);

                await _meteoriteRepository.AddAsync(meteoritesDto);

                await _fileService.DeletefileAsync(file);
            }
        }
    }
}
