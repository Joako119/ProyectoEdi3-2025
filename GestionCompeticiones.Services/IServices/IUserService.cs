using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.Services.IServices
{
    public interface IUserService
    {
        Task AsignarRol(string userId, string roleId);
    }
}

