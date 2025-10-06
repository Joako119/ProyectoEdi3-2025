using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class PersonalFederacion
    {

            public PersonalFederacion()
            {
                Asignaciones = new HashSet<AsignacionPersonalFiscalCarrera>();
            }

            public int Id { get; set; }

            [ForeignKey(nameof(Federacion))]
            public int FederacionId { get; set; }
            public virtual Federacion Federacion { get; set; } // <-- virtual

            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DNI { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public bool Activo { get; set; }

            public virtual ICollection<AsignacionPersonalFiscalCarrera> Asignaciones { get; set; } = new List<AsignacionPersonalFiscalCarrera>();
        }
    }

