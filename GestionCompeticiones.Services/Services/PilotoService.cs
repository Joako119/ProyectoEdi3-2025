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
    public class PilotoService : IPilotoService
    {
        private readonly IApplication<Piloto> _repo;
        private readonly IApplication<ResultadoCarrera> _resultadoRepo;
        private readonly UserManager<User> _userManager;

        public PilotoService(IApplication<Piloto> repo, IApplication<ResultadoCarrera> resultadoRepo, UserManager<User> userManager)
        {
            _repo = repo;
            _resultadoRepo = resultadoRepo;
            _userManager = userManager;
        }

        public async Task<IList<Piloto>> ObtenerTodos(User usuario)
        {
            var esAdminGeneral = await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral");
            var esAdminCategoria = await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria");
            var esUsuario = await _userManager.IsInRoleAsync(usuario, "Usuario");

            if (!esAdminGeneral && !esAdminCategoria && !esUsuario)
                throw new AccesoExcepcion("No tenés permisos para ver pilotos");

            return _repo.GetAll();
        }

        public async Task<Piloto> ObtenerPorId(int id)
        {
            var piloto = _repo.GetById(id);
            if (piloto == null)
                throw new NoEncontradoExcepcion("Piloto no encontrado");
            return piloto;
        }

        public async Task Crear(Piloto piloto, User usuario)
        {
            var esAdminGeneral = await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral");
            var esAdminCategoria = await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria");
            var esUsuario = await _userManager.IsInRoleAsync(usuario, "Usuario");

            if (!esAdminGeneral && !esAdminCategoria && !esUsuario)
                throw new AccesoExcepcion("No tenés permisos para crear pilotos");

            var errores = new List<string>();
            if (string.IsNullOrWhiteSpace(piloto.DNI)) errores.Add("El DNI del piloto es obligatorio");
            if (piloto.FechaNacimiento == default) errores.Add("La fecha de nacimiento es obligatoria");
            if (string.IsNullOrWhiteSpace(piloto.Nacionalidad)) errores.Add("La nacionalidad es obligatoria");
            if (errores.Any()) throw new ValidacionExcepcion(errores);

            var existeDni = _repo.GetAll().Any(p => p.DNI == piloto.DNI);
            if (existeDni)
                throw new ValidacionExcepcion(new[] { "Ya existe un piloto con ese DNI" });

            _repo.Save(piloto);
        }

        public async Task Editar(int id, Piloto pilotoNuevo)
        {
            var piloto = _repo.GetById(id);
            if (piloto == null)
                throw new NoEncontradoExcepcion("Piloto no encontrado");

            var errores = new List<string>();
            if (string.IsNullOrWhiteSpace(pilotoNuevo.DNI)) errores.Add("El DNI del piloto es obligatorio");
            if (pilotoNuevo.FechaNacimiento == default) errores.Add("La fecha de nacimiento es obligatoria");
            if (string.IsNullOrWhiteSpace(pilotoNuevo.Nacionalidad)) errores.Add("La nacionalidad es obligatoria");
            if (errores.Any()) throw new ValidacionExcepcion(errores);

            var existeDni = _repo.GetAll().Any(p => p.Id != id && p.DNI == pilotoNuevo.DNI);
            if (existeDni)
                throw new ValidacionExcepcion(new[] { "Ya existe un piloto con ese DNI" });

            piloto.DNI = pilotoNuevo.DNI;
            piloto.FechaNacimiento = pilotoNuevo.FechaNacimiento;
            piloto.Nacionalidad = pilotoNuevo.Nacionalidad;
            piloto.FotoPerfil = pilotoNuevo.FotoPerfil;
            piloto.LicenciaNumero = pilotoNuevo.LicenciaNumero;

            _repo.Save(piloto);
        }

        public async Task Borrar(int id)
        {
            var piloto = _repo.GetById(id);
            if (piloto == null)
                throw new NoEncontradoExcepcion("Piloto no encontrado");

          
            var resultados = _resultadoRepo.GetAll(); 
            var participa = resultados.Any(r => r.PilotoId == piloto.Id);

            if (participa)
                throw new ValidacionExcepcion(new[] { "No se puede borrar un piloto que participa en una carrera" });

            _repo.Delete(piloto.Id);
        }
    }
}