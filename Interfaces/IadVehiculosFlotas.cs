using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadVehiculosFlotas
    {
        public Task<IEnumerable<mensaje>> agregarVehiculo(int CompanyID, string PlacaNew, string NombreNew, string Marca, string Modelo, int FlotaID,
            float KmXGalon, float CapacidadTanque, int TipoCombustibleID, string VINNew, int CentroCostoID, int UserAsignaID);
        public Task<IEnumerable<FlotaModel>> obtenerFlotas(int CompanyID);
        public Task<IEnumerable<mensaje>> agregarFlota(int CompanyID, string SubfleetName);
        public Task<IEnumerable<rpVehiculoModel>> rpVehiculo(int CompanyID, string FechaIni, string FechaFin);
        public Task<IEnumerable<CombustibleMobileModel>> obtenerTipoCombustible(); 
        public Task<IEnumerable<mensaje>> ActualizaMobile(int MobileID, int CompanyID, string Placa, string Nombre, int FlotaID , string Marca, string Modelo, float? KmXGalon, float? CapTan, int? CombustibleID , string NumeroVIm);
        public Task<IEnumerable<mensaje>> EliminarMobile(int MobileID);
        public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID);
        public Task<IEnumerable<mensaje>> CrearCentroCosto(string Nombre, int CompanyID);
        public Task<IEnumerable<mensaje>> EliminarSubfleet(int SubfleetID, int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarSubfleet(int SubfleetID, int CompanyID, string NombreSubfleetNuevo);
        public Task<IEnumerable<MobileXSubfleet>> ObtenerMobileXSubfleet(int SubfleetID);
    }
}
