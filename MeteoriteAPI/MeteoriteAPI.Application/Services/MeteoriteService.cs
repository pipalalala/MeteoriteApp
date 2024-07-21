using AutoMapper;
using MeteoriteAPI.Common.Models.Filters;
using MeteoriteAPI.Domain.Repositories.Interfaces;
using MeteoriteAPI.Application.Models;
using MeteoriteAPI.Application.Services.Interfaces;

namespace MeteoriteAPI.Application.Services
{
    public class MeteoriteService : IMeteoriteService
    {
        private readonly IMapper _mapper;
        private readonly IMeteoriteRepository _meteoriteRepository;

        public MeteoriteService(
            IMapper mapper,
            IMeteoriteRepository meteoriteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _meteoriteRepository = meteoriteRepository
                ?? throw new ArgumentNullException(nameof(meteoriteRepository));
        }

        public async Task<IEnumerable<MeteoriteGroup>> GetByFilterAsync(MeteoriteFilter filters)
        {
            ArgumentNullException.ThrowIfNull(nameof(filters));

            var result = await _meteoriteRepository.GetByFilterAsync(filters);

            return _mapper.Map<IEnumerable<MeteoriteGroup>>(result);
        }

        public async Task<IEnumerable<int?>> GetYearRangeListAsync()
        {
            var result = await _meteoriteRepository.GetYearRangeListAsync();

            return result.Where(_ => _ != null).ToList();
        }

        public async Task<IEnumerable<string>> GetRecClassListAsync()
        {
            return await _meteoriteRepository.GetRecClassListAsync();
        }
    }
}
