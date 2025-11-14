using GestionCompeticiones.Application.Dtos.Identity.Roles;
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
    public class RolService : IRolService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IList<RoleResponseDto>> ObtenerTodos(User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral"))
                throw new AccesoExcepcion("No tenés permisos para ver roles");

            return _roleManager.Roles.Select(r => new RoleResponseDto { Id = r.Id, Name = r.Name }).ToList();
        }

        public async Task<Guid> Crear(RoleRequestDto dto, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral"))
                throw new AccesoExcepcion("No tenés permisos para crear roles");

            var role = new Role { Id = Guid.NewGuid(), Name = dto.Name };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                throw new ValidacionExcepcion(result.Errors.Select(e => e.Description));

            return role.Id;
        }

        public async Task Editar(Guid id, RoleRequestDto dto, User usuario)
        {
            if (!await _userManager.IsInRoleAsync(usuario, "AdministradorGeneral"))
                throw new AccesoExcepcion("No tenés permisos para editar roles");

            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                throw new NoEncontradoExcepcion("Rol no encontrado");

            role.Name = dto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                throw new ValidacionExcepcion(result.Errors.Select(e => e.Description));
        }

        public async Task<RoleResponseDto> ObtenerPorId(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                throw new NoEncontradoExcepcion("Rol no encontrado");

            return new RoleResponseDto { Id = role.Id, Name = role.Name };
        }
    }
}
