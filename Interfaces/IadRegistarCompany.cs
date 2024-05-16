using System.Collections.Generic;
using System.Threading.Tasks;
using COMBUSTIBLEAESCORE.Models;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadRegistarCompany
    {
        public Task<IEnumerable<mensaje>> RegisterCompany(string Nombre, string Apellido, string Username, string Clave, string Correo, string NombreCompany, string DireccionCompany);
    }
}
