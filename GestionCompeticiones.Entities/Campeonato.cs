using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Campeonato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Año { get; set; }
        public string ReglasPuntaje { get; set; }
        public EstadoCampeonato Estado { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Carrera> Carreras { get; set; }
        public ICollection<ParticipacionCampeonato> Participantes { get; set; }
        public ICollection<TablaPosiciones> TablaGeneral { get; set; }
    }
}
