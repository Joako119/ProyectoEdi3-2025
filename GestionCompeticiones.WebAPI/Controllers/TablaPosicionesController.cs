using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaPosicionesController : ControllerBase
    {
        private readonly ILogger<TablaPosicionesController> _logger;
        private readonly IApplication<TablaPosiciones> _tablaApp;

        public TablaPosicionesController(ILogger<TablaPosicionesController> logger, IApplication<TablaPosiciones> tablaApp)
        {
            _logger = logger;
            _tablaApp = tablaApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_tablaApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var tabla = _tablaApp.GetById(id.Value);
            if (tabla is null) return NotFound();
            return Ok(tabla);
        }

        [HttpPost]
        public IActionResult Crear(TablaPosiciones tabla)
        {
            if (!ModelState.IsValid) return BadRequest();
            _tablaApp.Save(tabla);
            return Ok(tabla.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, TablaPosiciones tabla)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _tablaApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.PuntajeTotal = tabla.PuntajeTotal;
            back.PosicionGeneral = tabla.PosicionGeneral;
            back.CarrerasDisputadas = tabla.CarrerasDisputadas;

            _tablaApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _tablaApp.GetById(id.Value);
            if (back is null) return NotFound();
            _tablaApp.Delete(back.Id);
            return Ok();
        }
    }
}
