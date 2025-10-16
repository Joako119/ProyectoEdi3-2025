using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Piloto
{
    public class PilotoRequestDto
    {
        public int Id { get; set; }

        public int PilotoId { get; set; }
 
        public int EquipoId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
