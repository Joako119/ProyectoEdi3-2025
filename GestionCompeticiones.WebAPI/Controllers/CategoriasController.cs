using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Categoria;
using GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CategoriaController : ControllerBase
        {
            private readonly ILogger<CategoriaController> _logger;
            private readonly IApplication<Categoria> _service;
            private readonly IMapper _mapper;

            public CategoriaController(
                ILogger<CategoriaController> logger,
                IApplication<Categoria> service,
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
                return Ok(_mapper.Map<IList<CategoriaResponseDto>>(_service.GetAll()));
            }

            [HttpGet]
            [Route("ById")]
            public async Task<IActionResult> ById(int? Id)
            {
                if (!Id.HasValue) return BadRequest();
                Categoria cat = _service.GetById(Id.Value);
                if (cat is null) return NotFound();
                return Ok(_mapper.Map<CategoriaResponseDto>(cat));
            }

            [HttpPost]
            public async Task<IActionResult> Crear(CategoriaRequestDto requestDto)
            {
                if (!ModelState.IsValid) return BadRequest();
                var cat = _mapper.Map<Categoria>(requestDto);
                _service.Save(cat);
                return Ok(cat.Id);
            }

            [HttpPut]
            public async Task<IActionResult> Editar(int? Id, CategoriaRequestDto requestDto)
            {
                if (!Id.HasValue) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();
                Categoria catBack = _service.GetById(Id.Value);
                if (catBack is null) return NotFound();
                catBack = _mapper.Map<Categoria>(requestDto);
                _service.Save(catBack);
                return Ok();
            }

            [HttpDelete]
            public async Task<IActionResult> Borrar(int? Id)
            {
                if (!Id.HasValue) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();
                Categoria catBack = _service.GetById(Id.Value);
                if (catBack is null) return NotFound();
                _service.Delete(catBack.Id);
                return Ok();
            }
        }
    }
}
