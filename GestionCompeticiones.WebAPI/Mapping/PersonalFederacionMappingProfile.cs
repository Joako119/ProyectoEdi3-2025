using AutoMapper;
using GestionCompeticiones.Application.Dtos.PersonalFederacion;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class PersonalFederacionMappingProfile : Profile
    {
        public PersonalFederacionMappingProfile()
        {
            CreateMap<PersonalFederacion, PersonalFederacionResponseDto>();
            CreateMap<PersonalFederacionRequestDto, PersonalFederacion>();
        }
    }

}
