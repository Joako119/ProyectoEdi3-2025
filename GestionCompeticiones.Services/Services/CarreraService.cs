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
    public class CarreraService : ICarreraService
    {

    private readonly IApplication<Carrera> _repo;
        private readonly UserManager<User> _userManager;

        public CarreraService(IApplication<Carrera> repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IList<Carrera>> ObtenerTodos(User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para ver carreras");

            return _repo.GetAll();
        }

        public async Task<Carrera> ObtenerPorId(int id)
        {
            var carrera = _repo.GetById(id);
            if (carrera == null)
                throw new NoEncontradoExcepcion("Carrera no encontrada");
            return carrera;
        }

        public async Task Crear(Carrera carrera, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para crear carreras");

            if (string.IsNullOrWhiteSpace(carrera.Nombre))
                throw new ValidacionExcepcion(new[] { "El nombre de la carrera es obligatorio" });

            _repo.Save(carrera);
        }

        public async Task Editar(int id, Carrera carreraNueva)
        {
            var carrera = _repo.GetById(id);
            if (carrera == null)
                throw new NoEncontradoExcepcion("Carrera no encontrada");

            carrera.Nombre = carreraNueva.Nombre;
            carrera.Fecha = carreraNueva.Fecha;
            carrera.Ubicacion = carreraNueva.Ubicacion;
            _repo.Save(carrera);
        }

        public async Task Borrar(int id)
        {
            var carrera = _repo.GetById(id);
            if (carrera == null)
                throw new NoEncontradoExcepcion("Carrera no encontrada");

            _repo.Delete(carrera.Id);
        }
    }
}