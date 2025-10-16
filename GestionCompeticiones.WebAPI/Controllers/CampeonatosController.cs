using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Campeonato;
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
        private readonly IApplication<Campeonato> _service;
        private readonly IMapper _mapper;

        public CampeonatosController(
            ILogger<CampeonatosController> logger,
            IApplication<Campeonato> service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<CampeonatoResponseDto>>(_service.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Campeonato comp = _service.GetById(Id.Value);
            if (comp is null) return NotFound();
            return Ok(_mapper.Map<CampeonatoResponseDto>(comp));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CampeonatoRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var comp = _mapper.Map<Campeonato>(requestDto);
            _service.Save(comp);
            return Ok(comp.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, CampeonatoRequestDto requestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Campeonato compBack = _service.GetById(Id.Value);
            if (compBack is null) return NotFound();
            compBack = _mapper.Map<Campeonato>(requestDto);
            _service.Save(compBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Campeonato compBack = _service.GetById(Id.Value);
            if (compBack is null) return NotFound();
            _service.Delete(compBack.Id);
            return Ok();
        }
    }
}
