using MeteoriteAPI.Domain.Models;
using MeteoriteAPI.Domain.Models.EF;
using MeteoriteAPI.Common.Models.Filters;

namespace MeteoriteAPI.Domain.Extensions
{
    public static class MeteoriteQueryExtensions
    {
        public static IQueryable<Meteorite> AddFilters(
            this IQueryable<Meteorite> query,
            MeteoriteFilter filters)
        {
            if (filters.Year is not null)
            {
                query = query.Where(_ =>
                    _.Year >= filters.Year.StartDateYear
                    && _.Year <= filters.Year.EndDateYear);
            }
            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(_ => _.Name.Contains(filters.Name));
            }
            if (!string.IsNullOrWhiteSpace(filters.RecClass))
            {
                query = query.Where(_ => _.RecClass == filters.RecClass);
            }

            return query;
        }

        public static IQueryable<MeteoriteGroup> AddSorting(
            this IQueryable<MeteoriteGroup> query,
            MeteoriteFilter filters)
        {
            switch ((filters?.Sorting?.SortBy, filters?.Sorting?.SortOrder))
            {
                case (SortBy.Year, SortOrder.Ascending):
                    {
                        query = query.OrderBy(_ => _.Year);
                        break;
                    }
                case (SortBy.Year, SortOrder.Descending):
                    {
                        query = query.OrderByDescending(_ => _.Year);
                        break;
                    }
                case (SortBy.Count, SortOrder.Ascending):
                    {
                        query = query.OrderBy(_ => _.Count);
                        break;
                    }
                case (SortBy.Count, SortOrder.Descending):
                    {
                        query = query.OrderByDescending(_ => _.Count);
                        break;
                    }
                case (SortBy.SumMass, SortOrder.Ascending):
                    {
                        query = query.OrderBy(_ => _.SumMass);
                        break;
                    }
                case (SortBy.SumMass, SortOrder.Descending):
                    {
                        query = query.OrderByDescending(_ => _.SumMass);
                        break;
                    }
                default:
                    break;
            }

            return query;
        }
    }
}
