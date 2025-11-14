using AutoMapper;
using GestionCompeticiones.Application;
using GestionCompeticiones.Application.Dtos.Carrera;
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
    public class CarreraController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CarreraController> _logger;
        private readonly IApplication<Carrera> _carrera;
        private readonly IMapper _mapper;

        public CarreraController(
            ILogger<CarreraController> logger,
            UserManager<User> userManager,
            IApplication<Carrera> carrera,
            IMapper mapper)
        {
            _logger = logger;
            _carrera = carrera;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria, Usuario, Piloto")]
        public async Task<IActionResult> All()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (_userManager.IsInRoleAsync(user, "Administrador").Result)
            {
                var name = User.FindFirst("name");
                var a = User.Claims;
                return Ok(_mapper.Map<IList<CarreraResponseDto>>(_carrera.GetAll()));
            }
            return Unauthorized();
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
            Carrera carrera = _carrera.GetById(Id.Value);
            if (carrera is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CarreraResponseDto>(carrera));
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Crear(CarreraRequestDto carreraRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var carrera = _mapper.Map<Carrera>(carreraRequestDto);
            _carrera.Save(carrera);
            return Ok(carrera.Id);
        }

        [HttpPut]
        [Route("Edit")]
        [Authorize(Roles = "AdministradorGeneral, AdministradorCategoria")]
        public async Task<IActionResult> Editar(int? Id, CarreraRequestDto carreraRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Carrera carreraBack = _carrera.GetById(Id.Value);
            if (carreraBack is null)
            { return NotFound(); }
            carreraBack = _mapper.Map<Carrera>(carreraRequestDto);
            _carrera.Save(carreraBack);
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
            Carrera carreraBack = _carrera.GetById(Id.Value);
            if (carreraBack is null)
            { return NotFound(); }
            _carrera.Delete(carreraBack.Id);
            return Ok();
        }
    }
}