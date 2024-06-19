using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAutorizarVales
    {
        public Task<IEnumerable<ValesGeneradosModel>> ObtenerValesGenerados(string FechaIni, string FechaFin, int CompanyID);
        public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID);
    }
}
