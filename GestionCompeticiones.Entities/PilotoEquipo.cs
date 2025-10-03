using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class PilotoEquipo
    {
        public int PilotoId { get; set; }
        public Piloto Piloto { get; set; }

        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
