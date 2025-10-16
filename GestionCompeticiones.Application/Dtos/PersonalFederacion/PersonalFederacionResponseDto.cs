using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.PersonalFederacion
{
    public class PersonalFederacionResponseDto
    {
        public int Id { get; set; }

      
        public int FederacionId { get; set; }
      

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }
}
