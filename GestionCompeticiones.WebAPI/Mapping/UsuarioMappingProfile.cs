using AutoMapper;
using GestionCompeticiones.Application.Dtos.Usuario;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>();
            CreateMap<UsuarioRequestDto, Usuario>();
        }
    }

}
