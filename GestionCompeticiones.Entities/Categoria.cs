using GestionCompeticiones.Entities.GestionCompeticiones.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Categoria
    {
       
        public Categoria()
        {
            Campeonatos = new HashSet<Campeonato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey(nameof(UsuarioResponsable))]
        public int UsuarioResponsableId { get; set; }
        public virtual Usuario UsuarioResponsable { get; set; } // virtual para proxies

        public virtual ICollection<Campeonato> Campeonatos { get; set; } // virtual para proxies

    }
}