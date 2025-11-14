using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionCompeticiones.Entities
{
    public class Categoria : IEntidad
    {
        public Categoria(string nombre)
        {
            SetNombre(nombre);
        }
        public Categoria()
        {
            Campeonatos = new HashSet<Campeonato>();
        }

        public int Id { get; set; }
        public string Nombre { get;private set; }
        public string Descripcion { get; set; }

       
        public virtual ICollection<Campeonato> Campeonatos { get; set; }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentNullException("El nombre de la categoria debe ser valido");
            }
            if (!Regex.IsMatch(nombre, @"^[A-Za-z0-9 ]+$"))
                throw new ArgumentException("El nombre solo puede contener letras y números.");

            Nombre = nombre;
        }
      

    }
}