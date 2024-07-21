using Microsoft.EntityFrameworkCore;
using MeteoriteAPI.Common.Models.Filters;
using MeteoriteAPI.Domain.Models;
using MeteoriteAPI.Domain.Models.EF;
using MeteoriteAPI.Domain.Extensions;
using MeteoriteAPI.Domain.Repositories.Interfaces;

namespace MeteoriteAPI.Domain.Repositories
{
    public class MeteoriteRepository : IMeteoriteRepository
    {
        private readonly MeteoritesContext _context;

        public MeteoriteRepository(MeteoritesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(IEnumerable<Meteorite> meteorites)
        {
            ArgumentNullException.ThrowIfNull(nameof(meteorites));

            await _context.Meteorites.AddRangeAsync(meteorites);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MeteoriteGroup>> GetByFilterAsync(MeteoriteFilter filters)
        {
            ArgumentNullException.ThrowIfNull(nameof(filters));

            var query = _context.Meteorites
                .AsQueryable()
                .AddFilters(filters)
                .GroupBy(_ => _.Year)
                .Select(_ => new MeteoriteGroup()
                {
                    Year = _.Key,
                    Count = _.Count(),
                    SumMass = _.Sum(_ => _.Mass)
                })
                .AddSorting(filters);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<int?>> GetYearRangeListAsync()
        {
            return await _context.Meteorites
                .Select(_ => _.Year)
                .Distinct()
                .Where(_ => _ != null)
                .OrderByDescending(_ => _)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRecClassListAsync()
        {
            return await _context.Meteorites
                .Select(_ => _.RecClass)
                .Distinct()
                .OrderBy(_ => _)
                .ToListAsync();
        }
    }
}
