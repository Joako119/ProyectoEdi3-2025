using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Federacion : IEntidad
    {
        public Federacion()
        {
            Personal = new HashSet<PersonalFederacion>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string EmailContacto { get; set; }
        public string Telefono { get; set; }
        public bool Activa { get; set; }

        public virtual ICollection<PersonalFederacion> Personal { get; set; } = new List<PersonalFederacion>();
    }
}
