using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Piloto;
using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PilotoController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PilotoController> _logger;
        private readonly IApplication<Piloto> _piloto;
        private readonly IMapper _mapper;

        public PilotoController(
            ILogger<PilotoController> logger,
            UserManager<User> userManager,
            IApplication<Piloto> piloto,
            IMapper mapper)
        {
            _logger = logger;
            _piloto = piloto;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> All()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (_userManager.IsInRoleAsync(user, "Administrador").Result)
            {
                var name = User.FindFirst("name");
                var a = User.Claims;
                return Ok(_mapper.Map<IList<PilotoResponseDto>>(_piloto.GetAll()));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Piloto piloto = _piloto.GetById(Id.Value);
            if (piloto is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PilotoResponseDto>(piloto));
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Crear(PilotoRequestDto pilotoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var piloto = _mapper.Map<Piloto>(pilotoRequestDto);
           piloto.Id = pilotoRequestDto.Id;

            _piloto.Save(piloto);
            return Ok(piloto.Id);
        }

        [HttpPut]
        [Route("Editar")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Editar(int? Id, PilotoRequestDto pilotoRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Piloto pilotoBack = _piloto.GetById(Id.Value);
            if (pilotoBack is null)
            { return NotFound(); }
            pilotoBack = _mapper.Map<Piloto>(pilotoRequestDto);
            _piloto.Save(pilotoBack);
            return Ok();
        }

        [HttpDelete]

        [Route("Borrar")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Piloto pilotoBack = _piloto.GetById(Id.Value);
            if (pilotoBack is null)
            { return NotFound(); }
            _piloto.Delete(Id.Value);
            return Ok();
        }
    }
}