using MeteoriteAPI.Application.Models;
using MeteoriteAPI.Common.Models.Filters;

namespace MeteoriteAPI.Application.Services.Interfaces
{
    public interface IMeteoriteService
    {
        Task<IEnumerable<MeteoriteGroup>> GetByFilterAsync(MeteoriteFilter filters);

        Task<IEnumerable<int?>> GetYearRangeListAsync();

        Task<IEnumerable<string>> GetRecClassListAsync();
    }
}
