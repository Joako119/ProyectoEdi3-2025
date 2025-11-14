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
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AsignarRol(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            if (user == null)
                throw new NoEncontradoExcepcion("Usuario no encontrado");
            if (role == null)
                throw new NoEncontradoExcepcion("Rol no encontrado");

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
                throw new ValidacionExcepcion(result.Errors.Select(e => e.Description));
        }
    }
}
