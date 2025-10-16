using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Usuario
{
    public class UsuarioRequestDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

    
    }
}
