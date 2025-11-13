using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class EstadisticaPiloto : IEntidad
    {
        public EstadisticaPiloto() { }
        #region properties
        public int Id { get; set; }

        [ForeignKey(nameof(Piloto))]
        public int PilotoId { get; set; }

        public virtual Piloto Piloto { get; set; }
        public int Temporada { get; set; }
        public string Categoria { get; set; }

        public int CarrerasCorridas { get; set; }
        public int Victorias { get; set; }
        public int Podios { get; set; }
        public int Abandonos { get; set; }
        public double PuntajePromedio { get; set; }
        #endregion

    
    }
}
