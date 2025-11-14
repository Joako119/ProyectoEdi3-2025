using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IFederacionService
    {
        Task<IList<Federacion>> ObtenerTodas(User usuario);
        Task<Federacion> ObtenerPorId(int id);
        Task Crear(Federacion federacion, User usuario);
        Task Editar(int id, Federacion federacionNueva);
        Task Borrar(int id);
    }
}
