using System.Text.Json.Serialization;

namespace MeteoriteAPI.Common.Models.Filters
{
    public class Sorting
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortBy? SortBy { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortOrder? SortOrder { get; set; }
    }
}
