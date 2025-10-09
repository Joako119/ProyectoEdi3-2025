using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerasController : ControllerBase
    {
        private readonly ILogger<CarrerasController> _logger;
        private readonly IApplication<Carrera> _carreraApp;

        public CarrerasController(ILogger<CarrerasController> logger, IApplication<Carrera> carreraApp)
        {
            _logger = logger;
            _carreraApp = carreraApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_carreraApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var carrera = _carreraApp.GetById(id.Value);
            if (carrera is null) return NotFound();
            return Ok(carrera);
        }

        [HttpPost]
        public IActionResult Crear(Carrera carrera)
        {
            if (!ModelState.IsValid) return BadRequest();
            _carreraApp.Save(carrera);
            return Ok(carrera.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, Carrera carrera)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _carreraApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.Nombre = carrera.Nombre;
            back.Fecha = carrera.Fecha;
            back.Ubicacion = carrera.Ubicacion;
            back.Resultados = carrera.Resultados;
            back.CampeonatoId = carrera.CampeonatoId;
            back.Campeonato=carrera.Campeonato;
            back.CampeonatoId = carrera.CampeonatoId;
            _carreraApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _carreraApp.GetById(id.Value);
            if (back is null) return NotFound();
            _carreraApp.Delete(back.Id);
            return Ok();
        }
    }
}
