using GestionCompeticiones.Application;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotosController : ControllerBase
    {
        private readonly ILogger<PilotosController> _logger;
        private readonly IApplication<Piloto> _pilotoApp;

        public PilotosController(ILogger<PilotosController> logger, IApplication<Piloto> pilotoApp)
        {
            _logger = logger;
            _pilotoApp = pilotoApp;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_pilotoApp.GetAll());
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var piloto = _pilotoApp.GetById(id.Value);
            if (piloto is null) return NotFound();
            return Ok(piloto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Piloto piloto)
        {
            if (!ModelState.IsValid) return BadRequest();
            _pilotoApp.Save(piloto);
            return Ok(piloto.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? id, Piloto piloto)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var pilotoBack = _pilotoApp.GetById(id.Value);
            if (pilotoBack is null) return NotFound();

            pilotoBack.DNI = piloto.DNI;
            pilotoBack.FechaNacimiento = piloto.FechaNacimiento;
            pilotoBack.Nacionalidad = piloto.Nacionalidad;
            pilotoBack.FotoPerfil = piloto.FotoPerfil;
            pilotoBack.LicenciaNumero = piloto.LicenciaNumero;

            _pilotoApp.Save(pilotoBack);
            return Ok(pilotoBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue || !ModelState.IsValid) return BadRequest();
            var pilotoBack = _pilotoApp.GetById(id.Value);
            if (pilotoBack is null) return NotFound();
            _pilotoApp.Delete(pilotoBack.Id);
            return Ok();
        }
    }
}
