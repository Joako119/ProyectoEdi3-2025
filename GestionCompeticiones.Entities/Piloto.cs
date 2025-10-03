using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Piloto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string FotoPerfil { get; set; }
        //public string LicenciaNumero { get; set; }

        public ICollection<PilotoEquipo> HistorialEquipos { get; set; }
        public ICollection<ParticipacionCampeonato> Campeonatos { get; set; }
        public ICollection<ResultadoCarrera> Resultados { get; set; }
        public ICollection<EstadisticaPiloto> Estadisticas { get; set; }
    }
}
