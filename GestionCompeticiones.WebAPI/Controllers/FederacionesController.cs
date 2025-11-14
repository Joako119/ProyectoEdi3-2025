using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Federacion;
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
    public class FederacionController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<FederacionController> _logger;
        private readonly IApplication<Federacion> _federacion;
        private readonly IMapper _mapper;

        public FederacionController(
            ILogger<FederacionController> logger,
            UserManager<User> userManager,
            IApplication<Federacion> federacion,
            IMapper mapper)
        {
            _logger = logger;
            _federacion = federacion;
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
                return Ok(_mapper.Map<IList<FederacionResponseDto>>(_federacion.GetAll()));
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
            Federacion federacion = _federacion.GetById(Id.Value);
            if (federacion is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FederacionResponseDto>(federacion));
        }

        [HttpPost]
        [Route("Crear")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Crear(FederacionRequestDto federacionRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var federacion = _mapper.Map<Federacion>(federacionRequestDto);
            _federacion.Save(federacion);
            return Ok(federacion.Id);
        }

        [HttpPut]
        [Route("Editar")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Editar(int? Id, FederacionRequestDto federacionRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Federacion federacionBack = _federacion.GetById(Id.Value);
            if (federacionBack is null)
            { return NotFound(); }
            federacionBack = _mapper.Map<Federacion>(federacionRequestDto);
            _federacion.Save(federacionBack);
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
            Federacion federacionBack = _federacion.GetById(Id.Value);
            if (federacionBack is null)
            { return NotFound(); }
            _federacion.Delete(federacionBack.Id);
            return Ok();
        }
    }
}