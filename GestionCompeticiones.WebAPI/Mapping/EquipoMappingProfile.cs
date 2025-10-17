using AutoMapper;
using GestionCompeticiones.Application.Dtos.Equipo;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class EquipoMappingProfile : Profile
    {
        public EquipoMappingProfile()
        {
            CreateMap<Equipo, EquipoResponseDto>();
            CreateMap<EquipoRequestDto, Equipo>();
        }
    }

}
