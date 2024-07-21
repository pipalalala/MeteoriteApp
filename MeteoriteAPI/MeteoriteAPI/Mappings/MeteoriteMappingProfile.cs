using AutoMapper;
using MeteoriteWeb = MeteoriteAPI.Web.Models;
using MeteoriteApplication = MeteoriteAPI.Application.Models;

namespace MeteoriteAPI.Web.Mappings
{
    public class MeteoriteMappingProfile : Profile
    {
        public MeteoriteMappingProfile()
        {
            CreateMap<MeteoriteApplication.MeteoriteGroup, MeteoriteWeb.MeteoriteGroup>()
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
    }
}
