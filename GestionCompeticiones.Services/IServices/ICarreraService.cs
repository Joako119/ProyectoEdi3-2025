using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface ICarreraService
    {

        Task<IList<Carrera>> ObtenerTodos(User usuario);
        Task<Carrera> ObtenerPorId(int id);
        Task Crear(Carrera carrera, User usuario);
        Task Editar(int id, Carrera carreraNueva);
        Task Borrar(int id);
    }
}
