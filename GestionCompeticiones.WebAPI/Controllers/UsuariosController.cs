using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Usuario;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IApplication<Usuario> _service;
        private readonly IMapper _mapper;

        public UsuarioController(
            ILogger<UsuarioController> logger,
            IApplication<Usuario> service,
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
            return Ok(_mapper.Map<IList<UsuarioResponseDto>>(_service.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            Usuario entity = _service.GetById(Id.Value);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<UsuarioResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<Usuario>(requestDto);
            _service.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, UsuarioRequestDto requestDto)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Usuario entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            entityBack = _mapper.Map<Usuario>(requestDto);
            _service.Save(entityBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Usuario entityBack = _service.GetById(Id.Value);
            if (entityBack is null) return NotFound();
            _service.Delete(entityBack.Id);
            return Ok();
        }
    }
}
