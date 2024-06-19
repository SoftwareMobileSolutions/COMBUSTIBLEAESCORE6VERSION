using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadGenerarVales
    {
        public Task<IEnumerable<MobileXCompanyModel>> ObtenerMobileXCompany(int CompanyID);
        public Task<IEnumerable<TipoCargaModel>> ObtenerTipoCarga();
        public Task<IEnumerable<ValesGeneradosModel>> ObtenerValesGenerados(string FechaIni, string FechaFin, int UsuarioID, int CompanyID);
        public Task<IEnumerable<mensaje>> GenerarVale(int MobileID, int CompanyID, string FechaGeneracion, int UserID, int TipoCargaValeID, float? TotalDolares, float? TotaGalones);
    }
}
