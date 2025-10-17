using AutoMapper;
using GestionCompeticiones.Application.Dtos.Federacion;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class FederacionMappingProfile : Profile
    {
        public FederacionMappingProfile()
        {
            CreateMap<Federacion, FederacionResponseDto>();
            CreateMap<FederacionRequestDto, Federacion>();
        }
    }

}
