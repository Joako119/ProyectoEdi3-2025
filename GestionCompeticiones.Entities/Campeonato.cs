using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Campeonato
    {
        public Campeonato()
        {
            Carreras = new HashSet<Carrera>();
            Pilotos = new HashSet<Piloto>();
            TablaGeneral = new HashSet<TablaPosiciones>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } // Ej: "Clase A", "Clase B"
        public int Año { get; set; }       // Ej: 2025
        public string ReglasPuntaje { get; set; }
        public EstadoCampeonato Estado { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int UsuarioResponsableId { get; set; } // Responsable específico del campeonato
        public Usuario UsuarioResponsable { get; set; }

        public ICollection<Carrera> Carreras { get; set; }
        public ICollection<Piloto> Pilotos { get; set; }
        public ICollection<TablaPosiciones> TablaGeneral { get; set; }
    }
}
