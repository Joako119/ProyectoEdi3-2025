using AutoMapper;
using GestionCompeticiones.Application.Dtos.Identity.User;
using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserMappingProfile>();
            CreateMap<UserRequestDto, User>();
        }
    }

}
