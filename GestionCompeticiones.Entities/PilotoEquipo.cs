using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class PilotoEquipo : IEntidad
    {
        public PilotoEquipo() { }

        public int Id { get; set; }

        public int PilotoId { get; set; }
        public virtual Piloto Piloto { get; set; }

        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
    }
