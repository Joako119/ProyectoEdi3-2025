using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private readonly ILogger<ResultadosController> _logger;
        private readonly IApplication<ResultadoCarrera> _resultadoApp;

        public ResultadosController(ILogger<ResultadosController> logger, IApplication<ResultadoCarrera> resultadoApp)
        {
            _logger = logger;
            _resultadoApp = resultadoApp;
        }

        [HttpGet("All")]
        public IActionResult All() => Ok(_resultadoApp.GetAll());

        [HttpGet("ById")]
        public IActionResult ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var resultado = _resultadoApp.GetById(id.Value);
            if (resultado is null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Crear(ResultadoCarrera resultado)
        {
            if (!ModelState.IsValid) return BadRequest();
            _resultadoApp.Save(resultado);
            return Ok(resultado.Id);
        }

        [HttpPut]
        public IActionResult Editar(int? id, ResultadoCarrera resultado)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _resultadoApp.GetById(id.Value);
            if (back is null) return NotFound();

            back.PosicionFinal = resultado.PosicionFinal;
            back.TiempoTotal = resultado.TiempoTotal;
            back.PuntajeObtenido = resultado.PuntajeObtenido;
            back.Abandono = resultado.Abandono;

            _resultadoApp.Save(back);
            return Ok(back);
        }

        [HttpDelete]
        public IActionResult Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var back = _resultadoApp.GetById(id.Value);
            if (back is null) return NotFound();
            _resultadoApp.Delete(back.Id);
            return Ok();
        }
    }
}
