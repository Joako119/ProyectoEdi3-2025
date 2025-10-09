using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly ILogger<EquiposController> _logger;
        private readonly IApplication<Equipo> _equipoApp;

        public EquiposController(ILogger<EquiposController> logger, IApplication<Equipo> equipoApp)
        {
            _logger = logger;
            _equipoApp = equipoApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_equipoApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var equipo = _equipoApp.GetById(id.Value);
            if (equipo is null) return NotFound();
            return Ok(equipo);
        }

        [HttpPost]
        public IActionResult Crear(Equipo equipo)
        {
            if (!ModelState.IsValid) return BadRequest();
            _equipoApp.Save(equipo);
            return Ok(equipo.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Equipo equipo)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _equipoApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Nombre = equipo.Nombre;
            back.Pais = equipo.Pais;
            back.Logo = equipo.Logo;
            back.FechaCreacion = equipo.FechaCreacion;

            _equipoApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _equipoApp.GetById(id.Value);
            if (back is null) return NotFound();
            _equipoApp.Delete(back.Id);
            return Ok();
        }
    }
}
