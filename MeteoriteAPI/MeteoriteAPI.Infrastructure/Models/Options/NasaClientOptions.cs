namespace MeteoriteAPI.Infrastructure.Models.Options
{
    public class NasaClientOptions
    {
        public const string SectionPath = "Clients:Nasa";

        public string? MeteoriteUrl { get; set; }
    }
}
