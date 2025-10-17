using AutoMapper;
using GestionCompeticiones.Application.Dtos.Campeonato;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class CampeonatoMappingProfile : Profile
    {
        public CampeonatoMappingProfile()
        {
            CreateMap<Campeonato, CampeonatoResponseDto>();
            CreateMap<CampeonatoRequestDto, Campeonato>();
        }
    }

}
