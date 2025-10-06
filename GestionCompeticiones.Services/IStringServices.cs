using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services
{
    public interface IStringServices
    {
        string GetString(string stn);
    }
    public class StringServices : IStringServices
    {
        public string GetString(string stn)
        {
            return string.Join(" ", stn, "Funciona");
        }
    }
}
