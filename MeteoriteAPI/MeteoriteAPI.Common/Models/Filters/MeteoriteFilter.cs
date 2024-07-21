namespace MeteoriteAPI.Common.Models.Filters
{
    public class MeteoriteFilter
    {
        public YearFilter? Year { get; set; }

        public string? RecClass { get; set; }

        public string? Name { get; set; }

        public Sorting? Sorting { get; set; }
    }
}
