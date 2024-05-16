using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadConfiguracionCompany
    {
        public Task<IEnumerable<MunicipioModel>> ObtenerMunicipios(int DepartamentoID);
        public Task<IEnumerable<DepartamentoModel>> ObtenerDepartamentos();
        public Task<IEnumerable<CompanyModel>> ObtenerDataCompany(int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarInformacion(int CompanyID, string NombreCompany, string Direccion, string SitioWeb, string Correo, string RazonSocial, int? DepartamentoID, int? MunicipioID, string Descripcion, string TelMovil, string Telfijo);
    }
}
