using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class AsignacionPersonalFiscalCarrera : IEntidad
    {
        public AsignacionPersonalFiscalCarrera() 
        {
        }

        public int Id { get; set; }

        [ForeignKey(nameof(PersonalFederacion))]
        public int PersonalFederacionId { get; set; }
        public virtual PersonalFederacion PersonalFederacion { get; set; }

        [ForeignKey(nameof(Carrera))]
        public int CarreraId { get; set; }
        public virtual Carrera Carrera { get; set; }


        public RolFiscal Rol { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Observaciones { get; set; }
    }
}
