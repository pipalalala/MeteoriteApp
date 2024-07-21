namespace MeteoriteAPI.Domain.Models.EF
{
    public class Meteorite
    {
        public int Id { get; set; }

        public int MeteoriteId { get; set; }

        public string Name { get; set; }

        public string NameType { get; set; }

        public string RecClass { get; set; }

        public decimal? Mass { get; set; }

        public string Fall { get; set; }

        public int? Year { get; set; }

        public decimal? RecLat { get; set; }

        public decimal? RecLong { get; set; }

        public string? GeolocationType { get; set; }

        public decimal? GeolocationCoordinatesLong { get; set; }

        public decimal? GeolocationCoordinatesLat { get; set; }
    }
}
