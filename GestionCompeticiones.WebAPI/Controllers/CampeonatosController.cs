using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampeonatosController : ControllerBase
    {
        private readonly ILogger<CampeonatosController> _logger;
        private readonly IApplication<Campeonato> _campeonatoApp;

        public CampeonatosController(ILogger<CampeonatosController> logger, IApplication<Campeonato> campeonatoApp)
        {
            _logger = logger;
            _campeonatoApp = campeonatoApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_campeonatoApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var campeonato = _campeonatoApp.GetById(id.Value);
            if (campeonato is null) return NotFound();
            return Ok(campeonato);
        }

        [HttpPost]
        public IActionResult Crear(Campeonato campeonato)
        {
            if (!ModelState.IsValid) return BadRequest();
            _campeonatoApp.Save(campeonato);
            return Ok(campeonato.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Campeonato campeonato)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _campeonatoApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Nombre = campeonato.Nombre;
            back.Año = campeonato.Año;
            back.ReglasPuntaje = campeonato.ReglasPuntaje;
            back.Estado = campeonato.Estado;
            back.CategoriaId = campeonato.CategoriaId;
            back.UsuarioResponsableId = campeonato.UsuarioResponsableId;

            _campeonatoApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _campeonatoApp.GetById(id.Value);
            if (back is null) return NotFound();
            _campeonatoApp.Delete(back.Id);
            return Ok();
        }
    }
}
