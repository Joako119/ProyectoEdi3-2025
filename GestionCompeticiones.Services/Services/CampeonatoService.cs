using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using GestionCompeticiones.Services.IServices;
using GestionCompeticiones.WebAPI.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.Services
{
    public class CampeonatoService : ICampeonatoService
    {
        private readonly IApplication<Campeonato> _repo;
        private readonly UserManager<User> _userManager;

        public CampeonatoService(IApplication<Campeonato> repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IList<Campeonato>> ObtenerTodos(User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para ver campeonatos");

            return _repo.GetAll();
        }

        public async Task<Campeonato> ObtenerPorId(int id)
        {
            var campeonato = _repo.GetById(id);
            if (campeonato == null)
                throw new NoEncontradoExcepcion("Campeonato no encontrado");
            return campeonato;
        }

        public async Task Crear(Campeonato campeonato, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para crear campeonatos");

            if (campeonato.anio <= 0)
                throw new ValidacionExcepcion(new[] { "El año del campeonato debe ser mayor a cero" });

            campeonato.SetNombre();
            _repo.Save(campeonato);
        }

        public async Task Editar(int id, Campeonato campeonatoNuevo)
        {
            var campeonato = _repo.GetById(id);
            if (campeonato == null)
                throw new NoEncontradoExcepcion("Campeonato no encontrado");

            campeonato.ReglasPuntaje = campeonatoNuevo.ReglasPuntaje;
            campeonato.Estado = campeonatoNuevo.Estado;
            campeonato.anio = campeonatoNuevo.anio;
            campeonato.SetNombre();

            _repo.Save(campeonato);
        }

        public async Task Borrar(int id)
        {
            var campeonato = _repo.GetById(id);
            if (campeonato == null)
                throw new NoEncontradoExcepcion("Campeonato no encontrado");

            _repo.Delete(campeonato.Id);
        }
    }
}