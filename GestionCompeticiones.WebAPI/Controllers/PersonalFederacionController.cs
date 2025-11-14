using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.PersonalFederacion;
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
    public class PersonalFederacionController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PersonalFederacionController> _logger;
        private readonly IApplication<PersonalFederacion> _personalFederacion;
        private readonly IMapper _mapper;

        public PersonalFederacionController(
            ILogger<PersonalFederacionController> logger,
            UserManager<User> userManager,
            IApplication<PersonalFederacion> personalFederacion,
            IMapper mapper)
        {
            _logger = logger;
            _personalFederacion = personalFederacion;
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
                return Ok(_mapper.Map<IList<PersonalFederacionResponseDto>>(_personalFederacion.GetAll()));
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
            PersonalFederacion personal = _personalFederacion.GetById(Id.Value);
            if (personal is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PersonalFederacionResponseDto>(personal));
        }

        [HttpPost]
        [Route("Crear")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Crear(PersonalFederacionRequestDto personalRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var personal = _mapper.Map<PersonalFederacion>(personalRequestDto);
            _personalFederacion.Save(personal);
            return Ok(personal.Id);
        }

        [HttpPut]
        [Route("Editar")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Editar(int? Id, PersonalFederacionRequestDto personalRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            PersonalFederacion personalBack = _personalFederacion.GetById(Id.Value);
            if (personalBack is null)
            { return NotFound(); }
            personalBack = _mapper.Map<PersonalFederacion>(personalRequestDto);
            _personalFederacion.Save(personalBack);
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
            PersonalFederacion personalBack = _personalFederacion.GetById(Id.Value);
            if (personalBack is null)
            { return NotFound(); }
            _personalFederacion.Delete(personalBack.Id);
            return Ok();
        }
    }
}