using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadVehiculosFlotas
    {
        public Task<IEnumerable<mensaje>> agregarVehiculo(int CompanyID, string PlacaNew, string NombreNew, string Marca, string Modelo, int FlotaID,
            float KmXGalon, float CapacidadTanque, int TipoCombustibleID, string VINNew);

        public Task<IEnumerable<FlotaModel>> obtenerFlotas(int CompanyID);

        public Task<IEnumerable<mensaje>> agregarFlota(int CompanyID, string SubfleetName);

        public Task<IEnumerable<rpVehiculoModel>> rpVehiculo(int CompanyID, string FechaIni, string FechaFin);

        public Task<IEnumerable<CombustibleMobileModel>> obtenerTipoCombustible();

    }
}
