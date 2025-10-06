using GestionCompeticiones.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCompeticiones.Entities
{
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
            public string Nombre { get; set; }
            public int Año { get; set; }
            public string ReglasPuntaje { get; set; }
            public EstadoCampeonato Estado { get; set; }

            [ForeignKey(nameof(Categoria))]
            public int CategoriaId { get; set; }
            public virtual Categoria Categoria { get; set; } // virtual

            [ForeignKey(nameof(UsuarioResponsable))]
            public int UsuarioResponsableId { get; set; }
            public virtual Usuario UsuarioResponsable { get; set; } // virtual

            public virtual ICollection<Carrera> Carreras { get; set; } // virtual
            public virtual ICollection<Piloto> Pilotos { get; set; } // virtual
            public virtual ICollection<TablaPosiciones> TablaGeneral { get; set; } // virtual
        }
    }
}
