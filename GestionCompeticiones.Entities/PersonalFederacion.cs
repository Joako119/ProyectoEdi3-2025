using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class PersonalFederacion : IEntidad
    {

            public PersonalFederacion()
            {
                Asignaciones = new HashSet<AsignacionPersonalFiscalCarrera>();
            }
        public PersonalFederacion(string nombre, string apellido, string dni, string email, string etlefono)
        {
            SetApellido(apellido);
            SetDni(dni);
            SetEmail(email);
            SetNumeroTEL(etlefono);
            SetNombre(nombre);
       
        }

        public int Id { get; set; }

            [ForeignKey(nameof(Federacion))]
            public int FederacionId { get; set; }
            public virtual Federacion Federacion { get; set; }

            public string Nombre { get;private set; }
            public string Apellido { get; private set; }
            public string DNI { get; private set; }
            public string Email { get; private set; }
            public string Telefono { get; private set; }
            public bool Activo { get; set; }

            public virtual ICollection<AsignacionPersonalFiscalCarrera> Asignaciones { get; set; } = new List<AsignacionPersonalFiscalCarrera>();


        #region getters y setters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentNullException("El nombre debe ser valido");
            }
            if (!Regex.IsMatch(nombre, @"^[A-Za-z ]+$"))
                throw new ArgumentException("El nombre solo puede contener letras.");

            Nombre = nombre;
        }
        public void SetApellido(string apellido)
        {
            if (string.IsNullOrEmpty(apellido))
            {
                throw new ArgumentNullException("El apellido debe ser valido");
            }
            if (!Regex.IsMatch(apellido, @"^[A-Za-z ]+$"))
                throw new ArgumentException("El apellido solo puede contener letras.");

            Apellido = apellido;
        }
        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("El pais debe ser valido");
            }
            
            Email = email;
        }
        public void SetDni(string dni)
        {
            if (string.IsNullOrEmpty(dni))
            {
                throw new ArgumentNullException("El DNI debe ser valido");
            }
            if (!Regex.IsMatch(DNI, @"^[0-9 ]+$"))
                throw new ArgumentException("El DNI solo puede contener numeros .");
            DNI = dni;
        }
        public void SetNumeroTEL(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                throw new ArgumentNullException("El Numero de contacto debe ser valido");
            }
            if (!Regex.IsMatch(num, @"^[0-9 ]+$"))
                throw new ArgumentException("El telefono de contecto solo puede contener números.");

            Telefono = num;
        }
        #endregion

    }
}

