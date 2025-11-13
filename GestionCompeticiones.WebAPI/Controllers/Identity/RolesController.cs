using AutoMapper;
using GestionCompeticiones.Application.Dtos.Identity.Roles;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionCompeticiones.WebAPI.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<Role> roleManager
            , ILogger<RolesController> logger
            , IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtiene una lista de todos los roles
        /// </summary>
        /// <returns>Lista de Rols</returns>
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "AdministradorGeneral")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<RoleResponseDto>>(_roleManager.Roles.ToList()));
        }

        /// <summary>
        /// Crea un rol
        /// </summary>
        /// <param name="{"></param>
        /// <returns></returns>
        /// <response code="200">Rol Creado</response>
        /// <response code="400">Error al validar el Rol</response>
        /// <response code="401">Permisos Invalidos. Comuniquese con el administrador</response>
        /// <response code="500"> No se pudo crear el rol</response>
        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "AdministradorGeneral")]
        public IActionResult Guardar(RoleRequestDto roleRequestDto)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);
                    role.Id = Guid.NewGuid();
                    var result = _roleManager.CreateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos.");
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Modificar([FromBody] RoleRequestDto roleRequestDto, [FromQuery] Guid id)/*Falta */
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);
                    role.Id = id;
                    var result = _roleManager.UpdateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos.");
            }
        }

        [Route("GetById")]
        [HttpGet]
        public IActionResult GetById(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest();
                }
                var role = _roleManager.FindByIdAsync(id.Value.ToString());
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<RoleResponseDto>(role));
            }
            catch (Exception ex)
            {
                return Conflict();
            }
        }
    }
}