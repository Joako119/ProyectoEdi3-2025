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
        public class PersonalFederacionService : IPersonalFederacionService
        {
            private readonly IApplication<PersonalFederacion> _repo;
            private readonly UserManager<User> _userManager;

            public PersonalFederacionService(IApplication<PersonalFederacion> repo, UserManager<User> userManager)
            {
                _repo = repo;
                _userManager = userManager;
            }

            public async Task<IList<PersonalFederacion>> ObtenerTodos(User usuario)
            {
                if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral"))
                    throw new AccesoExcepcion("No tenés permisos para ver personal de federación");

                return _repo.GetAll();
            }

            public async Task<PersonalFederacion> ObtenerPorId(int id)
            {
                var personal = _repo.GetById(id);
                if (personal == null)
                    throw new NoEncontradoExcepcion("Personal de federación no encontrado");
                return personal;
            }

            public async Task Crear(PersonalFederacion personal, User usuario)
            {
                if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral"))
                    throw new AccesoExcepcion("No tenés permisos para crear personal de federación");

                var errores = new List<string>();
                if (string.IsNullOrWhiteSpace(personal.Nombre)) errores.Add("El nombre es obligatorio");
                if (string.IsNullOrWhiteSpace(personal.Apellido)) errores.Add("El apellido es obligatorio");
                if (string.IsNullOrWhiteSpace(personal.DNI)) errores.Add("El DNI es obligatorio");
                if (errores.Any()) throw new ValidacionExcepcion(errores);

                var existeDni = _repo.GetAll().Any(p => p.DNI == personal.DNI && p.FederacionId == personal.FederacionId);
                if (existeDni) throw new ValidacionExcepcion(new[] { "Ya existe personal con ese DNI en la federación" });

                _repo.Save(personal);
            }

            public async Task Editar(int id, PersonalFederacion personalNuevo)
            {
                var personal = _repo.GetById(id);
                if (personal == null)
                    throw new NoEncontradoExcepcion("Personal de federación no encontrado");

                var errores = new List<string>();
                if (string.IsNullOrWhiteSpace(personalNuevo.Nombre)) errores.Add("El nombre es obligatorio");
                if (string.IsNullOrWhiteSpace(personalNuevo.Apellido)) errores.Add("El apellido es obligatorio");
                if (string.IsNullOrWhiteSpace(personalNuevo.DNI)) errores.Add("El DNI es obligatorio");
                if (errores.Any()) throw new ValidacionExcepcion(errores);

                var existeDni = _repo.GetAll().Any(p => p.Id != id && p.DNI == personalNuevo.DNI && p.FederacionId == personal.FederacionId);
                if (existeDni) throw new ValidacionExcepcion(new[] { "Ya existe personal con ese DNI en la federación" });

                personal.SetNombre(personalNuevo.Nombre) ;
                personal.SetNombre(personalNuevo.Apellido);
                personal.SetNombre(personalNuevo.DNI);
                personal.SetNombre(personalNuevo.Email);
                personal.SetNombre(personalNuevo.Telefono);
                personal.Activo = personalNuevo.Activo;
             
                _repo.Save(personal);
            }

            public async Task Borrar(int id)
            {
                var personal = _repo.GetById(id);
                if (personal == null)
                    throw new NoEncontradoExcepcion("Personal de federación no encontrado");

                var tieneAsignaciones = personal.Asignaciones != null && personal.Asignaciones.Any();
                if (tieneAsignaciones)
                    throw new ValidacionExcepcion(new[] { "No se puede borrar personal con asignaciones. Remové las asignaciones primero" });

                _repo.Delete(personal.Id);
            }
        }
}
