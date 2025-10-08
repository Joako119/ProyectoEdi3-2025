using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class ResultadoCarrera : IEntidad
    {
            public ResultadoCarrera() { }

            public int Id { get; set; }
        [ForeignKey(nameof(Carrera))]
        public int CarreraId { get; set; }
        public virtual Carrera Carrera { get; set; }

        [ForeignKey(nameof(Piloto))]
        public int PilotoId { get; set; }
        public virtual Piloto Piloto { get; set; }


        public int PosicionFinal { get; set; }
            public TimeSpan TiempoTotal { get; set; }
            public int PuntajeObtenido { get; set; }
            public bool Abandono { get; set; }
        }
    }

