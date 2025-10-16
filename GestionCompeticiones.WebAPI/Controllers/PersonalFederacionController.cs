using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.PersonalFederacion;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalFederacionController : ControllerBase
    {
        private readonly ILogger<PersonalFederacionController> _logger;
        private readonly IApplication<PersonalFederacion> _service;
        private readonly IMapper _mapper;

        public PersonalFederacionController(
            ILogger<PersonalFederacionController> logger,
            IApplication<PersonalFederacion> service,
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
            return Ok(_mapper.Map<IList<PersonalFederacionResponseDto>>(_service.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            PersonalFederacion entity = _service.GetById(Id.Value);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<PersonalFederacionResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PersonalFederacionRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<PersonalFederacion>(requestDto);
            _service.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, PersonalFederacionRequestDto requestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            PersonalFederacion entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            entityBack = _mapper.Map<PersonalFederacion>(requestDto);
            _service.Save(entityBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            PersonalFederacion entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            _service.Delete(entityBack.Id);
            return Ok();
        }
    }
}
