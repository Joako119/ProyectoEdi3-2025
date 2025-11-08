using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities.MicrosoftIdentity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            CategoriasResponsables = new HashSet<Categoria>();
        }
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100)]
        [PersonalData]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100)]
        [PersonalData]
        public string Apellidos { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual Piloto? Piloto { get; set; }

        public virtual ICollection<Categoria> CategoriasResponsables { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
