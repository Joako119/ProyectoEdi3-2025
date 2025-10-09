using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasPilotoController : ControllerBase
    {
        private readonly ILogger<EstadisticasPilotoController> _logger;
        private readonly IApplication<EstadisticaPiloto> _estadisticaApp;

        public EstadisticasPilotoController(ILogger<EstadisticasPilotoController> logger, IApplication<EstadisticaPiloto> estadisticaApp)
        {
            _logger = logger;
            _estadisticaApp = estadisticaApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_estadisticaApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var estadistica = _estadisticaApp.GetById(id.Value);
            if (estadistica is null) return NotFound();
            return Ok(estadistica);
        }

        [HttpPost]
        public IActionResult Crear(EstadisticaPiloto estadistica)
        {
            if (!ModelState.IsValid) return BadRequest();
            _estadisticaApp.Save(estadistica);
            return Ok(estadistica.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, EstadisticaPiloto estadistica)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _estadisticaApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Temporada = estadistica.Temporada;
            back.Categoria = estadistica.Categoria;
            back.CarrerasCorridas = estadistica.CarrerasCorridas;
            back.Victorias = estadistica.Victorias;
            back.Podios = estadistica.Podios;
            back.Abandonos = estadistica.Abandonos;
            back.PuntajePromedio = estadistica.PuntajePromedio;

            _estadisticaApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _estadisticaApp.GetById(id.Value);
            if (back is null) return NotFound();
            _estadisticaApp.Delete(back.Id);
            return Ok();
        }
    }
}

