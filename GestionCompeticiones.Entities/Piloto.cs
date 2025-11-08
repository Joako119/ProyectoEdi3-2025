using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Piloto : IEntidad
    {
        public Piloto()
        {
            HistorialEquipos = new HashSet<PilotoEquipo>();
            Campeonatos = new HashSet<Campeonato>();
            Estadisticas = new HashSet<EstadisticaPiloto>();
            Resultados = new HashSet<ResultadoCarrera>();
            TablaPosiciones = new HashSet<TablaPosiciones> ();
        }

        public int Id { get; set; }

            [ForeignKey(nameof(Usuario))]
            public Guid UsuarioId { get; set; } 
            public virtual User Usuario { get; set; } 

            public string DNI { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Nacionalidad { get; set; }
            public string FotoPerfil { get; set; }

        public int LicenciaNumero { get; set; }

        public virtual ICollection<PilotoEquipo> HistorialEquipos { get; set; }

        public virtual ICollection<Campeonato> Campeonatos { get; set; }
            public virtual ICollection<ResultadoCarrera> Resultados { get; set; }
            public virtual ICollection<EstadisticaPiloto> Estadisticas { get; set; }
            public virtual ICollection<TablaPosiciones> TablaPosiciones { get; set; } // FIX: Agregar propiedad faltante
     
    }
}
