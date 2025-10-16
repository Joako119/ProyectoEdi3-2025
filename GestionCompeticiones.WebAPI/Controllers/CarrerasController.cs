using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Carrera;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly ILogger<CarreraController> _logger;
        private readonly IApplication<Carrera> _service;
        private readonly IMapper _mapper;

        public CarreraController(
            ILogger<CarreraController> logger,
            IApplication<Carrera> service,
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
            return Ok(_mapper.Map<IList<CarreraResponseDto>>(_service.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Carrera entity = _service.GetById(Id.Value);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<CarreraResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CarreraRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<Carrera>(requestDto);
            _service.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, CarreraRequestDto requestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Carrera entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            entityBack = _mapper.Map<Carrera>(requestDto);
            _service.Save(entityBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Carrera entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            _service.Delete(entityBack.Id);
            return Ok();
        }

    }
}
