using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Equipo;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ILogger<EquipoController> _logger;
        private readonly IApplication<Equipo> _service;
        private readonly IMapper _mapper;

        public EquipoController(
            ILogger<EquipoController> logger,
            IApplication<Equipo> service,
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
            return Ok(_mapper.Map<IList<EquipoResponseDto>>(_service.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Equipo entity = _service.GetById(Id.Value);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<EquipoResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EquipoRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<Equipo>(requestDto);
            _service.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, EquipoRequestDto requestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Equipo entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            entityBack = _mapper.Map<Equipo>(requestDto);
            _service.Save(entityBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Equipo entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            _service.Delete(entityBack.Id);
            return Ok();
        }
    }
}
