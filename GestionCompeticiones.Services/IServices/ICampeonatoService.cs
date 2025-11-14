using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface ICampeonatoService
    {
        Task<IList<Campeonato>> ObtenerTodos(User usuario);
        Task<Campeonato> ObtenerPorId(int id);
        Task Crear(Campeonato campeonato, User usuario);
        Task Editar(int id, Campeonato campeonatoNuevo);
        Task Borrar(int id);
    }
}
