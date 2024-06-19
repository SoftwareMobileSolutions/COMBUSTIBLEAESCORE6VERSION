using System.Collections.Generic;
using System.Threading.Tasks;
using COMBUSTIBLEAESCORE.Models;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadUsuariosVehiculos
    {
        public Task<IEnumerable<ArbolUsuariosVehiculoModel>> ArbolUsuariosVehiculo(int CompanyID);

        public Task<IEnumerable<UserXcompanyModel>> UserXcompany(int CompanyID);

        public Task<IEnumerable<MobileXUserModel>> mobileXUser(string Username, int CompanyID);

        public Task<IEnumerable<MobileAsigandosXUserModel>> obtenerObtenerMobileAsigandosXUser(string username, int CompanyID);

        public Task<IEnumerable<mensaje>> ActulizarMobileXUser(string mobilesid, string Username);
    }
}
