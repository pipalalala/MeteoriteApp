namespace MeteoriteAPI.Application.Models.Clients.Nasa
{
    public class Meteorite
    {
        public string? Name { get; set; }

        public int Id { get; set; }

        public string? Nametype { get; set; }

        public string? Recclass { get; set; }

        public string? Mass { get; set; }

        public string? Fall { get; set; }

        public DateTime? Year { get; set; }

        public string? Reclat { get; set; }

        public string? Reclong { get; set; }

        public Geolocation? Geolocation { get; set; }
    }

    public class Geolocation
    {
        public string? Type { get; set; }

        public IEnumerable<double?> Coordinates { get; set; } = new List<double?>();
    }
}
