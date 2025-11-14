using GestionCompeticiones.Application.Dtos.Identity.Roles;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IRolService
    {
        Task<IList<RoleResponseDto>> ObtenerTodos(User usuario);
        Task<Guid> Crear(RoleRequestDto dto, User usuario);
        Task Editar(Guid id, RoleRequestDto dto, User usuario);
        Task<RoleResponseDto> ObtenerPorId(Guid id);
    }
}
