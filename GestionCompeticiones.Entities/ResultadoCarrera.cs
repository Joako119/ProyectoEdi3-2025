using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class ResultadoCarrera
    {
        public int Id { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        public int PilotoId { get; set; }
        public Piloto Piloto { get; set; }

        public int PosicionFinal { get; set; }
        public TimeSpan TiempoTotal { get; set; }
        public int PuntajeObtenido { get; set; }
        public bool Abandono { get; set; }
    }
}
