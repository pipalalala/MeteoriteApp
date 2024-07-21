using MeteoriteAPI.Domain.Models;
using MeteoriteAPI.Domain.Models.EF;
using MeteoriteAPI.Common.Models.Filters;

namespace MeteoriteAPI.Domain.Repositories.Interfaces
{
    public interface IMeteoriteRepository
    {
        Task AddAsync(IEnumerable<Meteorite> meteorites);

        Task<IEnumerable<MeteoriteGroup>> GetByFilterAsync(MeteoriteFilter filters);

        Task<IEnumerable<int?>> GetYearRangeListAsync();

        Task<IEnumerable<string>> GetRecClassListAsync();
    }
}
