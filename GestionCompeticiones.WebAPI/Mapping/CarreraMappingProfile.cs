using AutoMapper;
using GestionCompeticiones.Application.Dtos.Carrera;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class CarreraMappingProfile : Profile
    {
        public CarreraMappingProfile()
        {
            CreateMap<Carrera, CarreraResponseDto>();
            CreateMap<CarreraRequestDto, Carrera>();
        }
    }

}
