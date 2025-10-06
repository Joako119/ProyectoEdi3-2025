using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Equipo
    {
        public Equipo()
        {
            Pilotos = new HashSet<PilotoEquipo>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Logo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<PilotoEquipo> Pilotos { get; set; }
    }
}
