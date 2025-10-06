using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class PilotoEquipo
    {
       
            public PilotoEquipo() { }

            public int Id { get; set; }

            [ForeignKey(nameof(Piloto))]
            public int PilotoId { get; set; }
            public virtual Piloto Piloto { get; set; } // virtual para proxies

            [ForeignKey(nameof(Equipo))]
            public int EquipoId { get; set; }
            public virtual Equipo Equipo { get; set; } // virtual para proxies

            public DateTime FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
        }
    }
