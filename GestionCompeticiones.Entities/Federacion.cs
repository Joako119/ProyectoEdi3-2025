using GestionCompeticiones.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionCompeticiones.Entities
{
    public class Federacion : IEntidad
    {
        public Federacion(string nombre,string pais,string num,string email)
        {
            SetEmais(email); 
            SetNombre(nombre);
            SetPais(pais);
            SetNumeroTEL(num);
            
        }
        public Federacion()
        {
            Personal = new HashSet<PersonalFederacion>();
        }
        #region Properties
        public int Id { get; set; }
        public string Nombre { get; private set; }
        public string Pais { get; private set; }
        public string EmailContacto { get; private set; }
        public string Telefono { get; private set; }
        public bool Activa { get; set; }
        #endregion
        #region Virtual
        public virtual ICollection<PersonalFederacion> Personal { get; set; } = new List<PersonalFederacion>();
        #endregion

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
        public void SetPais(string pais)
        {
            if (string.IsNullOrEmpty(pais))
            {
                throw new ArgumentNullException("El pais debe ser valido");
            }
            if (!Regex.IsMatch(pais, @"^[A-Za-z ]+$"))
                throw new ArgumentException("El pais solo puede contener letras .");

            Pais = pais;
        }
        public void SetEmais(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("El Email debe ser valido");
            }
            EmailContacto = email;
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
