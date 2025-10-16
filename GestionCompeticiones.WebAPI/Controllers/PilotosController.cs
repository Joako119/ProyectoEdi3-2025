using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Piloto;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PilotoController : ControllerBase
    {
        private readonly ILogger<PilotoController> _logger;
        private readonly IApplication<Piloto> _service;
        private readonly IMapper _mapper;

        public PilotoController(
            ILogger<PilotoController> logger,
            IApplication<Piloto> service,
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
            return Ok(_mapper.Map<IList<PilotoResponseDto>>(_service.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Piloto entity = _service.GetById(Id.Value);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<PilotoResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PilotoRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<Piloto>(requestDto);
            _service.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, PilotoRequestDto requestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Piloto entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            entityBack = _mapper.Map<Piloto>(requestDto);
            _service.Save(entityBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Piloto entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            _service.Delete(entityBack.Id);
            return Ok();
        }
    }
}
