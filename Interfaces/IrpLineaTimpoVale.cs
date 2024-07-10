using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IrpLineaTimpoVale
    {
        public Task<IEnumerable<rpLineaTimpoValeModel>> ObtenerVales(int CompanyID, int MobileID, string FechaIni, string FechaFin);
        public Task<IEnumerable<MobileXCompanyModel>> ObtenerPlacas(int CompanyID);
    }
}
