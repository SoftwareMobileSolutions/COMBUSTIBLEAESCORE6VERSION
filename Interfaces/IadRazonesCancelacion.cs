using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadRazonesCancelacion
    {
        public Task<IEnumerable<mensaje>> crearRazonCancelacion(string Razon, int CompanyID);
        public Task<IEnumerable<RazonCancelacionModel>> ObtenerRazonCancelacion(int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarRazonCancelacion(int RazonValeCanceladoID, int CompanyID, string RazonNuevo);
        public Task<IEnumerable<mensaje>> EliminarRazonCancelacion(int RazonValeCanceladoID, int CompanyID);
    }
}
