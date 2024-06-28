using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAutorizarVales
    {
        public Task<IEnumerable<ValesGeneradosModel>> ObtenerValesGenerados(string FechaIni, string FechaFin, int CompanyID);
        //public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID);
        public Task<IEnumerable<ProyectoModel>> ObtenerProyectos(int CompanyID);
        public Task<IEnumerable<ValesGeneradosModel>> ObtenerValeAutorizar(int ValeID, int CompanyID);
        public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCostoAutorizalVale(string Placa, int CompanyID, int UserID);
        public Task<IEnumerable<mensaje>> AutorizarVale(int ValeCombustibleID, int TipoCargaID, int CentroCostoID, float? CantidadGalones, float? TotalPrecio, int ProyectoID, int UsuarioAutorizadorID);

    }
}
