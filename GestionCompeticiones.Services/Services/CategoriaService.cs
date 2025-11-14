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
    public class CategoriaService : ICategoriaService
    {
        private readonly IApplication<Categoria> _repo;
        private readonly UserManager<User> _userManager;

        public CategoriaService(IApplication<Categoria> repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IList<Categoria>> ObtenerTodas(User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para ver categorías");

            return _repo.GetAll();
        }

        public async Task<Categoria> ObtenerPorId(int id)
        {
            var categoria = _repo.GetById(id);
            if (categoria == null)
                throw new NoEncontradoExcepcion("Categoría no encontrada");
            return categoria;
        }

        public async Task Crear(Categoria categoria, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral") &&
                !await _userManager.IsInRoleAsync(usuario, "AdministradorCategoria"))
                throw new AccesoExcepcion("No tenés permisos para crear categorías");

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new ValidacionExcepcion(new[] { "El nombre de la categoría es obligatorio" });

            var existe = _repo.GetAll().Any(c => c.Nombre == categoria.Nombre);
            if (existe)
                throw new ValidacionExcepcion(new[] { "Ya existe una categoría con ese nombre" });

            _repo.Save(categoria);
        }

        public async Task Editar(int id, Categoria categoriaNueva)
        {
            var categoria = _repo.GetById(id);
            if (categoria == null)
                throw new NoEncontradoExcepcion("Categoría no encontrada");

            categoria.SetNombre(categoriaNueva.Nombre);
            categoria.Descripcion = categoriaNueva.Descripcion;
            _repo.Save(categoria);
        }

        public async Task Borrar(int id)
        {
            var categoria = _repo.GetById(id);
            if (categoria == null)
                throw new NoEncontradoExcepcion("Categoría no encontrada");
            _repo.Delete(categoria.Id);
        }
    }
}
