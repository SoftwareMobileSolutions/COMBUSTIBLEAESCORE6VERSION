using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadCentroCosto
    {
        public Task<IEnumerable<mensaje>> crearCentroCosto(string Nombre, int CompanyID);

        public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID);

        public Task<IEnumerable<mensaje>> ActualizarCentrosCosto(int CentroCostoID, int CompanyID, string NombreNuevo);

        public Task<IEnumerable<mensaje>> EliminarCentroCosto(int CentroCostoID, int CompanyID);
    }
}
