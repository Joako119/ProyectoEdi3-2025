using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCompeticiones.Entities
{
    namespace GestionCompeticiones.Entities
    {
        public class Campeonato : IEntidad
        {
            public Campeonato() { }

            public int Id { get; set; }

            public string Nombre { get; set; }
            public int Año { get; set; }
            public string ReglasPuntaje { get; set; }
            public EstadoCampeonato Estado { get; set; }

            [ForeignKey(nameof(Categoria))]
            public int CategoriaId { get; set; }
            public virtual Categoria Categoria { get; set; }

            [ForeignKey(nameof(Federacion))]
            public int FederacionId { get; set; }
            public virtual Federacion Federacion { get; set; }

            [ForeignKey(nameof(UsuarioResponsable))]
            public int UsuarioResponsableId { get; set; }
            public virtual Usuario UsuarioResponsable { get; set; }

            public virtual ICollection<Carrera> Carreras { get; set; } 
            public virtual ICollection<Piloto> Pilotos { get; set; } 
            public virtual ICollection<TablaPosiciones> TablaGeneral { get; set; } 
        }
    }
}
