using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Carrera : IEntidad
    {
     
            public Carrera()
            {
                Resultados = new HashSet<ResultadoCarrera>();
                Fiscales = new HashSet<AsignacionPersonalFiscalCarrera>();
            }

            public int Id { get; set; }

            [ForeignKey(nameof(Campeonato))]
            public int CampeonatoId { get; set; }
            public virtual Campeonato Campeonato { get; set; } 

            public string Nombre { get; set; }
            public DateTime Fecha { get; set; }
            public string Ubicacion { get; set; }


            public virtual ICollection<ResultadoCarrera> Resultados { get; set; } = new List<ResultadoCarrera>(); 

            public virtual ICollection<AsignacionPersonalFiscalCarrera> Fiscales { get; set; } = new List<AsignacionPersonalFiscalCarrera>(); 
        }
    }

