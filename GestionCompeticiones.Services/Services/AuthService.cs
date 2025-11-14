using GestionCompeticiones.Application.Dtos.Identity.User;
using GestionCompeticiones.Application.Dtos.Login;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using GestionCompeticiones.Services.AuthServices;
using GestionCompeticiones.Services.IServices;
using GestionCompeticiones.WebAPI.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace GestionCompeticiones.Services.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<User> _userManager;
        private readonly ITokenHandlerService _tokenService;

        public AuthService(UserManager<User> userManager, ITokenHandlerService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UserResponseDto> RegistrarUsuario(UserRequestDto dto)
        {
            var existeUsuario = await _userManager.FindByEmailAsync(dto.Email);
            if (existeUsuario != null)
                throw new ValidacionExcepcion(new[] { "Ya existe un usuario con ese email" });

            var creado = await _userManager.CreateAsync(new User()
            {
                Email = dto.Email,
                UserName = dto.Email.Substring(0, dto.Email.IndexOf('@')),
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                FechaNacimiento = dto.FechaNacimiento
            }, dto.Password);

            if (!creado.Succeeded)
                throw new ValidacionExcepcion(creado.Errors.Select(e => e.Description));

            return new UserResponseDto
            {
                NombreCompleto = $"{dto.Nombres} {dto.Apellidos}",
                Email = dto.Email,
                UserName = dto.Email.Substring(0, dto.Email.IndexOf('@'))
            };
        }

        public async Task<LoginUserResponseDto> Login(LoginUserRequestDto dto)
        {
            var usuario = await _userManager.FindByEmailAsync(dto.Email);
            if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, dto.Password))
                throw new ValidacionExcepcion(new[] { "Usuario o contraseña incorrectos" });

            var roles = await _userManager.GetRolesAsync(usuario);
            var parametros = new TokenParameters()
            {
                Id = usuario.Id.ToString(),
                PaswordHash = usuario.PasswordHash,
                UserName = usuario.UserName,
                Email = usuario.Email,
                Roles = roles
            };
            var jwt = _tokenService.GenerateJwtTokens(parametros);

            return new LoginUserResponseDto
            {
                Login = true,
                Token = jwt,
                UserName = usuario.UserName,
                Mail = usuario.Email
            };
        }
    }
}
