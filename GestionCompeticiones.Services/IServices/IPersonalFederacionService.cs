using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IPersonalFederacionService
    {
        Task<IList<PersonalFederacion>> ObtenerTodos(User usuario);
        Task<PersonalFederacion> ObtenerPorId(int id);
        Task Crear(PersonalFederacion personal, User usuario);
        Task Editar(int id, PersonalFederacion personalNuevo);
        Task Borrar(int id);
    }
}
