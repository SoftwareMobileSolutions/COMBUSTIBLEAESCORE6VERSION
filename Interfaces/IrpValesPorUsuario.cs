using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IrpValesPorUsuario
    {
       public Task<IEnumerable<ValesModel>> ObtenerDataValesGeneral(string FechaIni, string FechaFin, int CompanyID, int PerfilUsuarioID);
       public Task<IEnumerable<ValesModel>> ObtenerDataVales(string FechaIni, string FechaFin, int CompanyID,/*string Username,*/ int PerfilUsuarioID);
    }
}
