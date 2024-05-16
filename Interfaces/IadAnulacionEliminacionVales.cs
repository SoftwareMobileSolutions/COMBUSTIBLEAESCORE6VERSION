using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAnulacionEliminacionVales
    {
        public Task<IEnumerable<rpObtenerValesCancelarAnular>> ObtenerValesCancelarAnular(int CompanyID);
        public Task<IEnumerable<mensaje>> EliminarVales(int ValeCombustubibleID, int CompanyID);
        public Task<IEnumerable<RazonCancelacionModel>> ObtenerRazonCancelacion(int CompanyID);
        public Task<IEnumerable<mensaje>> AnularVale(int ValeCombustubibleID, int RazonID ,int CompanyID);
        public Task<IEnumerable<mensaje>> ReestablecerVale(int ValeCombustubibleID, int CompanyID);
    }
}
