using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FederacionesController : ControllerBase
    {
        private readonly ILogger<FederacionesController> _logger;
        private readonly IApplication<Federacion> _federacionApp;

        public FederacionesController(ILogger<FederacionesController> logger, IApplication<Federacion> federacionApp)
        {
            _logger = logger;
            _federacionApp = federacionApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_federacionApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var federacion = _federacionApp.GetById(id.Value);
            if (federacion is null) return NotFound();
            return Ok(federacion);
        }

        [HttpPost]
        public IActionResult Crear(Federacion federacion)
        {
            if (!ModelState.IsValid) return BadRequest();
            _federacionApp.Save(federacion);
            return Ok(federacion.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Federacion federacion)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _federacionApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Nombre = federacion.Nombre;
            back.Pais = federacion.Pais;
            back.EmailContacto = federacion.EmailContacto;
            back.Telefono = federacion.Telefono;
            back.Activa = federacion.Activa;

            _federacionApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _federacionApp.GetById(id.Value);
            if (back is null) return NotFound();
            _federacionApp.Delete(back.Id);
            return Ok();
        }
    }
}
