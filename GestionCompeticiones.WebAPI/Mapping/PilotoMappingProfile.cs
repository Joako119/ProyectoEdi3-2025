using AutoMapper;
using GestionCompeticiones.Application.Dtos.Piloto;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class PilotoMappingProfile : Profile
    {
        public PilotoMappingProfile()
        {
            CreateMap<Piloto, PilotoResponseDto>()
                .ForMember(dest => dest.FechaNacimiento, ori => ori.MapFrom(src => src.FechaNacimiento.ToShortDateString()));
            CreateMap<PilotoRequestDto, Piloto>();
        }
    }

}
