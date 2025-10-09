using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IApplication<Usuario> _usuarioApp;

        public UsuariosController(ILogger<UsuariosController> logger, IApplication<Usuario> usuarioApp)
        {
            _logger = logger;
            _usuarioApp = usuarioApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_usuarioApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var usuario = _usuarioApp.GetById(id.Value);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            _usuarioApp.Save(usuario);
            return Ok(usuario.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Usuario usuario)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _usuarioApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Nombre = usuario.Nombre;
            back.Apellido = usuario.Apellido;
            back.Email = usuario.Email;
            back.Contraseña = usuario.Contraseña;
            back.Rol = usuario.Rol;
            back.Activo = usuario.Activo;

            _usuarioApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _usuarioApp.GetById(id.Value);
            if (back is null) return NotFound();
            _usuarioApp.Delete(back.Id);
            return Ok();

        }
    }
}
