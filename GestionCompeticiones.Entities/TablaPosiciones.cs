using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class TablaPosiciones
    {
        public int Id { get; set; }
        public int CampeonatoId { get; set; }
        public Campeonato Campeonato { get; set; }

        public int PilotoId { get; set; }
        public Piloto Piloto { get; set; }

        public int PuntajeTotal { get; set; }
        public int PosicionGeneral { get; set; }
        public int CarrerasDisputadas { get; set; }
    }
}
