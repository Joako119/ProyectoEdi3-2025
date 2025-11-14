using GestionCompeticiones.Application.Dtos.Identity.User;
using GestionCompeticiones.Application.Dtos.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegistrarUsuario(UserRequestDto dto);
        Task<LoginUserResponseDto> Login(LoginUserRequestDto dto);
    }
}
