using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Carrera
{
    public class CarreraResponseDto
    {
        public int Id { get; set; }

        public int CampeonatoId { get; set; }

        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }
    }
}
