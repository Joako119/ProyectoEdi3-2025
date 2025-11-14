using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IPilotoService
    {
        Task<IList<Piloto>> ObtenerTodos(User usuario);
        Task<Piloto> ObtenerPorId(int id);
        Task Crear(Piloto piloto, User usuario);
        Task Editar(int id, Piloto pilotoNuevo);
        Task Borrar(int id);
    }
}
