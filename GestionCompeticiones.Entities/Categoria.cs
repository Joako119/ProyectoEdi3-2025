using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Categoria : IEntidad
    {

       public Categoria()
        {
            Campeonatos = new HashSet<Campeonato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey(nameof(UsuarioResponsable))]
        public Guid  UsuarioResponsableId { get; set; }
        public virtual User UsuarioResponsable { get; set; }

        public virtual ICollection<Campeonato> Campeonatos { get; set; }
    

    }
}