using AutoMapper;
using MeteoriteDomain = MeteoriteAPI.Domain.Models;
using MeteoriteApplication = MeteoriteAPI.Application.Models;

namespace MeteoriteAPI.Application.Mappings
{
    public class MeteoriteMappingProfile : Profile
    {
        public MeteoriteMappingProfile()
        {
            CreateMap<MeteoriteApplication.Clients.Nasa.Meteorite, MeteoriteDomain.EF.Meteorite>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => default(int)))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.MeteoriteId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.NameType,
                    opt => opt.MapFrom(src => src.Nametype))
                .ForMember(
                    dest => dest.RecClass,
                    opt => opt.MapFrom(src => src.Recclass))
                .ForMember(
                    dest => dest.Mass,
                    opt => opt.ConvertUsing(new StringToDecimalConverter(), src => src.Mass))
                .ForMember(
                    dest => dest.Fall,
                    opt => opt.MapFrom(src => src.Fall))
                .ForMember(
                    dest => dest.Year,
                    opt => opt.ConvertUsing(new StringToYearConverter(), src => src.Year))
                .ForMember(
                    dest => dest.RecLat,
                    opt => opt.ConvertUsing(new StringToDecimalConverter(), src => src.Reclat))
                .ForMember(
                    dest => dest.RecLong,
                    opt => opt.ConvertUsing(new StringToDecimalConverter(), src => src.Reclong))
                .ForMember(
                    dest => dest.GeolocationType,
                    opt => opt.MapFrom(src => src.Geolocation.Type))
                .ForMember(
                    dest => dest.GeolocationCoordinatesLong,
                    opt => opt.MapFrom(src => src.Geolocation.Coordinates.FirstOrDefault()))
                .ForMember(
                    dest => dest.GeolocationCoordinatesLat,
                    opt => opt.MapFrom(src => src.Geolocation.Coordinates.LastOrDefault()));

            CreateMap<MeteoriteDomain.MeteoriteGroup, MeteoriteApplication.MeteoriteGroup>()
                .ForMember(
                    dest => dest.Year,
                    opt => opt.MapFrom(src => src.Year))
                .ForMember(
                    dest => dest.Count,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(
                    dest => dest.SumMass,
                    opt => opt.MapFrom(src => src.SumMass));
        }

        public class StringToDecimalConverter : IValueConverter<string?, decimal?>
        {
            public decimal? Convert(string? sourceMember, ResolutionContext context)
            {
                return string.IsNullOrWhiteSpace(sourceMember)
                    ? null
                    : decimal.Parse(sourceMember);
            }
        }

        public class StringToYearConverter : IValueConverter<DateTime?, int?>
        {
            public int? Convert(DateTime? sourceMember, ResolutionContext context)
            {
                return sourceMember.HasValue
                    ? sourceMember.Value.Year
                    : null;
            }
        }
    }
}
