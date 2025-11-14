using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Application.Dtos.Campeonato
{
    public class CampeonatoRequestDto
    {
     

        public string ReglasPuntaje { get; set; }
        public EstadoCampeonato Estado { get; set; }

        [ForeignKey(nameof(Categoria))]
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(Federacion))]
        public int FederacionId { get; set; }

        public int anio { get; set; }

    }
}
