using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Piloto
{
    public class PilotoResponseDto
    {
         public int Id { get; set; }

        
        public int UsuarioId { get; set; }

        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string FotoPerfil { get; set; }

        public int LicenciaNumero { get; set; }
    }
}
