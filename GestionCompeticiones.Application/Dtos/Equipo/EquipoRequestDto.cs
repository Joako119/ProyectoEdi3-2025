using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Equipo
{
    public class EquipoRequestDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Logo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
