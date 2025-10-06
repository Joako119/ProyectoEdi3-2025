using System;
using System.Collections.Generic;
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
        public string Nombre { get; set; } //  turismo pista
        public string Descripcion { get; set; } //  peso minimo 1800kg 

        public int UsuarioResponsableId { get; set; }
        public Usuario UsuarioResponsable { get; set; }

        public ICollection<Campeonato> Campeonatos { get; set; }
    }
}