using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class EstadisticaPiloto
    {
        public int Id { get; set; }
        public int PilotoId { get; set; }
        public Piloto Piloto { get; set; }

        public int Temporada { get; set; }
        public string Categoria { get; set; }

        public int CarrerasCorridas { get; set; }
        public int Victorias { get; set; }
        public int Podios { get; set; }
        public int Abandonos { get; set; }
        public double PuntajePromedio { get; set; }
    }
}
