using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Federacion
{
    public class FederacionRequestDto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Pais { get; set; }

        [EmailAddress]
        public string EmailContacto { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public bool Activa { get; set; }
    }
}
