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
    public class EquipoService : IEquipoService
    {

        private readonly IApplication<Equipo> _repo;
        private readonly UserManager<User> _userManager;

        public EquipoService(IApplication<Equipo> repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IList<Equipo>> ObtenerTodos(User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para ver equipos");

            return _repo.GetAll();
        }

        public async Task<Equipo> ObtenerPorId(int id)
        {
            var equipo = _repo.GetById(id);
            if (equipo == null)
                throw new NoEncontradoExcepcion("Equipo no encontrado");
            return equipo;
        }

        public async Task Crear(Equipo equipo, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para crear equipos");

            if (string.IsNullOrWhiteSpace(equipo.Nombre))
                throw new ValidacionExcepcion(new[] { "El nombre del equipo es obligatorio" });

            var existe = _repo.GetAll().Any(e => e.Nombre == equipo.Nombre);
            if (existe)
                throw new ValidacionExcepcion(new[] { "Ya existe un equipo con ese nombre" });

            _repo.Save(equipo);
        }

        public async Task Editar(int id, Equipo equipoNuevo)
        {
            var equipo = _repo.GetById(id);
            if (equipo == null)
                throw new NoEncontradoExcepcion("Equipo no encontrado");

            equipo.Nombre = equipoNuevo.Nombre;
            equipo.Pais = equipoNuevo.Pais;
            equipo.Logo = equipoNuevo.Logo;
            equipo.FechaCreacion = equipoNuevo.FechaCreacion;
            _repo.Save(equipo);
        }

        public async Task Borrar(int id)
        {
            var equipo = _repo.GetById(id);
            if (equipo == null)
                throw new NoEncontradoExcepcion("Equipo no encontrado");
            _repo.Delete(equipo.Id);
        }
    }
}