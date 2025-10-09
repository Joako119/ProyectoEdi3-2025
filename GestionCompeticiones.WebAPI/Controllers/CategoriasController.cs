using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly IApplication<Categoria> _categoriaApp;

        public CategoriasController(ILogger<CategoriasController> logger, IApplication<Categoria> categoriaApp)
        {
            _logger = logger;
            _categoriaApp = categoriaApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_categoriaApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var categoria = _categoriaApp.GetById(id.Value);
            if (categoria is null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Crear(Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest();
            _categoriaApp.Save(categoria);
            return Ok(categoria.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Categoria categoria)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _categoriaApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Nombre = categoria.Nombre;
            back.Descripcion = categoria.Descripcion;
            back.UsuarioResponsableId = categoria.UsuarioResponsableId;

            _categoriaApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _categoriaApp.GetById(id.Value);
            if (back is null) return NotFound();
            _categoriaApp.Delete(back.Id);
            return Ok();
        }
    }
}
