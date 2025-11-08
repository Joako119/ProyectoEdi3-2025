using AutoMapper;
using GestionCompeticiones.Application.Dtos.Identity.Roles;
using GestionCompeticiones.Entities.MicrosoftIdentity;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
