using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Campeonato
{
    public class CampeonatoResponseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ReglasPuntaje { get; set; }
        public EstadoCampeonato Estado { get; set; }
    }
}
