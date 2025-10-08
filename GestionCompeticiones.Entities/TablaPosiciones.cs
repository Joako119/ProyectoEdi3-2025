using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Entities.GestionCompeticiones.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class TablaPosiciones : IEntidad
    {
        public TablaPosiciones() { }

        public int Id { get; set; }

        [ForeignKey(nameof(Campeonato))]
        public int CampeonatoId { get; set; }
        public virtual Campeonato Campeonato { get; set; }

        [ForeignKey(nameof(Piloto))]
        public int PilotoId { get; set; }
        public virtual Piloto Piloto { get; set; }

        public int PuntajeTotal { get; set; }
        public int PosicionGeneral { get; set; }
        public int CarrerasDisputadas { get; set; }
    }
}
