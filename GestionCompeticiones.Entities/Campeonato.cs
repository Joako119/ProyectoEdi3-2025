using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Enums;
using System.ComponentModel.DataAnnotations.Schema;

    namespace GestionCompeticiones.Entities
    {
        public class Campeonato : IEntidad
        {
            public Campeonato()
            {
                Carreras = new HashSet<Carrera>();
                Pilotos = new HashSet<Piloto>();
                TablaGeneral = new HashSet<TablaPosiciones>();
            }
        public Campeonato(Categoria categoria)
        {
            SetCategoria(categoria);
            SetNombre();
        }
        #region properties
        public int Id { get; set; }

            public string Nombre { get;  set; }
           
            public string ReglasPuntaje { get; set; }
            public EstadoCampeonato Estado { get; set; }

            [ForeignKey(nameof(Categoria))]
            public int CategoriaId { get; set; }
  
            [ForeignKey(nameof(Federacion))]
            public int FederacionId { get; set; }
       
            public int anio {  get; set; }
        
        #endregion

        #region virtual
        public virtual Categoria Categoria { get; set; }
        public virtual Federacion Federacion { get; set; }
        public virtual ICollection<Carrera> Carreras { get; set; } 
            public virtual ICollection<Piloto> Pilotos { get; set; } 
            public virtual ICollection<TablaPosiciones> TablaGeneral { get; set; }
        #endregion




        public void SetNombre()
        {
            if (Categoria == null || string.IsNullOrWhiteSpace(Categoria.Nombre))
                throw new InvalidOperationException("La categoría debe estar asignada y tener un nombre válido.");

            if (anio <= 0)
                throw new InvalidOperationException("El año del campeonato debe ser mayor a cero.");

            Nombre = $"Campeonato {Categoria.Nombre} {anio}";
        }
        public void SetCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            Categoria = categoria;
            CategoriaId = categoria.Id;
        }
    }
}


