using COMBUSTIBLEAESCORE.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadCambioEstadoVale
    {
        public Task<IEnumerable<MobileXCompanyModel>> ObtenerMobilesXCompany(int CompanyID);
        public Task<IEnumerable<UsuarioModel>> ObtenerUsuarioXCompany(int CompanyID);
        public Task<IEnumerable<ValesModel>> ObtenerVales(int CompanyID, int FiltroID, string NumVale,int? MobileID, string FechaIni, string FechaFin, int? UsuarioID); 
        public Task<IEnumerable<EstadoValeModel>> ObtenerEstadosVale();
        public Task<IEnumerable<mensaje>> CambiarEstadoVales(string ValesID, int EstadoID, int UsuarioID, string CreditoFiscal, string FechaCreditoFiscal);
    }
}
