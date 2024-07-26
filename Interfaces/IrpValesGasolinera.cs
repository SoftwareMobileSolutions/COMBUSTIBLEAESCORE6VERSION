using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IrpValesGasolinera
    {
        public Task<IEnumerable<ValesXGasolineraModel>> ObtenerValesXGasolinera(int CompanyID, string FechaIni, string FechaFin);
    }
}
