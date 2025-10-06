using GestionCompeticiones.Entities.GestionCompeticiones.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Piloto
    {
       
            public Piloto()
            {
                HistorialEquipos = new HashSet<PilotoEquipo>();
                Campeonatos = new HashSet<Campeonato>();
                Resultados = new HashSet<ResultadoCarrera>();
                Estadisticas = new HashSet<EstadisticaPiloto>();
            }

            public int Id { get; set; }

            [ForeignKey(nameof(Usuario))]
            public int UsuarioId { get; set; }

            public virtual Usuario Usuario { get; set; } 

            public string DNI { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Nacionalidad { get; set; }
            public string FotoPerfil { get; set; }

            public virtual ICollection<PilotoEquipo> HistorialEquipos { get; set; }
            public virtual ICollection<Campeonato> Campeonatos { get; set; }
            public virtual ICollection<ResultadoCarrera> Resultados { get; set; }
            public virtual ICollection<EstadisticaPiloto> Estadisticas { get; set; }
    }
}
