using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAdministracionPerfiles
    {
        public Task<IEnumerable<PerfilModel>> ObtenerPefiles(int CompanyID);
        public Task<IEnumerable<ModulosAsignadosModel>> ObtenerModulos(int PerfilID, int CompanyID);
        public Task<IEnumerable<mensaje>> CrearPerfil(string NombrePefil, int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarPerfilXModulos(int CompanyID, int PerfilID, string NombrePerfil_Nuevo, string ModulosID);
    }
}
