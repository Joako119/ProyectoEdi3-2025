using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Carrera
    {
        public int Id { get; set; }
        public int CampeonatoId { get; set; }
        public Campeonato Campeonato { get; set; }

        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }
        //public EstadoCarrera Estado { get; set; }

        // Resultados de la carrera
        public ICollection<ResultadoCarrera> Resultados { get; set; } = new List<ResultadoCarrera>();

        // Lista de personas asignadas a la carrera (fiscales, veedores, administrativos, etc.)
        public ICollection<AsignacionPersonalFiscalCarrera> Fiscales { get; set; } = new List<AsignacionPersonalFiscalCarrera>();
    }
}
