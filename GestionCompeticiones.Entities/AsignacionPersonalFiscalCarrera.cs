using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class AsignacionPersonalFiscalCarrera
    {
        public int Id { get; set; }

        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        public int PersonalFederacionId { get; set; }
        public PersonalFederacion Personal { get; set; }

        public RolFiscal Rol { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Observaciones { get; set; }
    }
}
