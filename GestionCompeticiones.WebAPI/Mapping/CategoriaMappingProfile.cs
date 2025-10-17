using AutoMapper;
using GestionCompeticiones.Application.Dtos.Categoria;
using GestionCompeticiones.Entities;

namespace GestionCompeticiones.WebAPI.Mapping
{
    public class CategoriaMappingProfile : Profile
    {
        public CategoriaMappingProfile()
        {
            CreateMap<Categoria, CategoriaResponseDto>();
            CreateMap<CategoriaRequestDto, Categoria>();
        }
    }
}
