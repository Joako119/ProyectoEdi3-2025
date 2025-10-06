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
    public class Usuario
    {
        public Usuario()
        {
            CategoriasResponsables = new HashSet<Categoria>();
        }

        public int Id { get; set; }
        [StringLength(40)]
        public string Nombre { get; set; }
        [StringLength(40)]
        public string Apellido { get; set; }
        public MailAddress MailAddress { get; set; }

        public string Contraseña { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Piloto? Piloto { get; set; }
        public ICollection<Categoria>? CategoriasResponsables { get; set; }



       
       


    }
}
