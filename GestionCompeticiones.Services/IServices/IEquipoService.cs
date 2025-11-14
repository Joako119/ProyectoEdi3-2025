using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IEquipoService
    {
        Task<IList<Equipo>> ObtenerTodos(User usuario);
        Task<Equipo> ObtenerPorId(int id);
        Task Crear(Equipo equipo, User usuario);
        Task Editar(int id, Equipo equipoNuevo);
        Task Borrar(int id);
    }
}
