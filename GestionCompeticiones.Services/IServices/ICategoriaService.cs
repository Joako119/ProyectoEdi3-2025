using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface ICategoriaService
    {

        Task<IList<Categoria>> ObtenerTodas(User usuario);
        Task<Categoria> ObtenerPorId(int id);
        Task Crear(Categoria categoria, User usuario);
        Task Editar(int id, Categoria categoriaNueva);
        Task Borrar(int id);
    }
}
