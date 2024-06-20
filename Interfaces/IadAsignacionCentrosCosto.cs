using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAsignacionCentrosCosto
    {
        public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID);
        public Task<IEnumerable<UsuarioModel>> ObtenerUsuarios(int CompanyID);
        public Task<IEnumerable<CentroCostoXUserModel>> ObtenerCentroCostoXUser(int CompanyID, int UsuarioID);
    }
}
