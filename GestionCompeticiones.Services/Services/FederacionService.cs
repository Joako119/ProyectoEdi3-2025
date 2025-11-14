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
    public class FederacionService : IFederacionService
    {
        private readonly IApplication<Federacion> _repo;
        private readonly UserManager<User> _userManager;

        public FederacionService(IApplication<Federacion> repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IList<Federacion>> ObtenerTodas(User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para ver federaciones");

            return _repo.GetAll();
        }

        public async Task<Federacion> ObtenerPorId(int id)
        {
            var federacion = _repo.GetById(id);
            if (federacion == null)
                throw new NoEncontradoExcepcion("Federación no encontrada");
            return federacion;
        }

        public async Task Crear(Federacion federacion, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para crear federaciones");

            if (string.IsNullOrWhiteSpace(federacion.Nombre))
                throw new ValidacionExcepcion(new[] { "El nombre de la federación es obligatorio" });

            _repo.Save(federacion);
        }

        public async Task Editar(int id, Federacion federacionNueva)
        {
            var federacion = _repo.GetById(id);
            if (federacion == null)
                throw new NoEncontradoExcepcion("Federación no encontrada");

            federacion.SetNombre(federacionNueva.Nombre);
            federacion.SetPais(federacionNueva.Pais);
            federacion.SetEmais(federacionNueva.EmailContacto);
            federacion.SetNumeroTEL(federacionNueva.Telefono);
            _repo.Save(federacion);
        }

        public async Task Borrar(int id)
        {
            var federacion = _repo.GetById(id);
            if (federacion == null)
                throw new NoEncontradoExcepcion("Federación no encontrada");
            _repo.Delete(federacion.Id);
        }
    }
}

