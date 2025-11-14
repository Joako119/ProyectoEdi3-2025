using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Campeonato;
using GestionCompeticiones.DataAccess;
using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCompeticiones.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
     [Route("api/[controller]")]
    [ApiController]
    public class CampeonatoController : ControllerBase
    {
    
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CampeonatoController> _logger;
        private readonly IApplication<Campeonato> _campeonato;
        private readonly IMapper _mapper;
        private readonly IApplication<Categoria> _categoria;
        private readonly IApplication<Federacion> _federacion;

        public CampeonatoController(
     ILogger<CampeonatoController> logger,
     UserManager<User> userManager,
     IApplication<Campeonato> campeonato,
     IApplication<Categoria> categoria,
     IApplication<Federacion> federacion,
     IMapper mapper)
        {
            _logger = logger;
            _campeonato = campeonato;
            _mapper = mapper;
            _userManager = userManager;
            _categoria = categoria;
            _federacion = federacion;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria, Usuario, Piloto")]
        public async Task<IActionResult> All()
        {
            
                var id = User.FindFirst("Id")?.Value;
                if (id == null)
                    return Unauthorized();

                var user = await _userManager.FindByIdAsync(id);

                // Ya con el atributo Authorize, no hace falta volver a chequear roles aquí
                var campeonatos = _campeonato.GetAll(); // si tenés versión async, usá await _campeonato.GetAllAsync()
                var response = _mapper.Map<IList<CampeonatoResponseDto>>(campeonatos);

                return Ok(response);
           
        }
            [HttpGet]
        [Route("ById")]

        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria, Usuario, Piloto")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Campeonato campeonato = _campeonato.GetById(Id.Value);
            if (campeonato is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CampeonatoResponseDto>(campeonato));
        }

        [HttpPost]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Post([FromBody] CampeonatoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = _categoria.GetById(dto.CategoriaId);
            var federacion = _federacion.GetById(dto.FederacionId);

            if (categoria == null || federacion == null)
                return BadRequest("Categoría o federación no encontrada.");

            var campeonato = new Campeonato
            {
                anio = dto.anio,
                ReglasPuntaje = dto.ReglasPuntaje,
                Estado = dto.Estado,
                FederacionId = dto.FederacionId,
                Federacion = federacion,
               
            };

            campeonato.SetCategoria(categoria);
            campeonato.SetNombre();

            _campeonato.Save(campeonato);

            var response = _mapper.Map<CampeonatoResponseDto>(campeonato);
            return CreatedAtAction(nameof(ById), new { Id = campeonato.Id }, response);
        }

        [HttpPut]

        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Editar(int? Id, CampeonatoRequestDto campeonatoRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Campeonato campeonatoBack = _campeonato.GetById(Id.Value);
            if (campeonatoBack is null)
            { return NotFound(); }
            campeonatoBack = _mapper.Map<Campeonato>(campeonatoRequestDto);
            _campeonato.Save(campeonatoBack);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Campeonato campeonatoBack = _campeonato.GetById(Id.Value);
            if (campeonatoBack is null)
            { return NotFound(); }
            _campeonato.Delete(campeonatoBack.Id);
            return Ok();
        }
    }
}
