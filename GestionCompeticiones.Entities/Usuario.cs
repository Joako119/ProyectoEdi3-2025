using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Usuario : IEntidad
    {
        public Usuario()
        {
            CategoriasResponsables = new HashSet<Categoria>();
        }

        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual Piloto? Piloto { get; set; } 

        public virtual ICollection<Categoria> CategoriasResponsables { get; set; }
    }
}
    
