using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IrpTopValePorVehiculo
    {
        public Task<IEnumerable<rpTopValePorVehiculoModel>> ObtenerTopValesPorVehiculo(int NumTop, int CompanyID, string FechaIni, string FechaFin);
        public Task<IEnumerable<ValeUsadoXPlacaModel>> ObtenerValesUsadosXPlaca(int CompanyID, /*int MobileID*/ string Placa, string FechaIncio, string FechaFin);
    }
}
