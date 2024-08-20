using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadGasolinera
    {
        public Task<IEnumerable<GasolineraModel>> ObtenerGasolineras(int CompanyID);
        public Task<IEnumerable<mensaje>> CrearGasolinera(int CompanyID, string DescriEstacionServicio, float Latitud, float Longitud,string CodigoGasolinera, string CiudadGasolinera, string GerenteGasolinera, string TelefonoGasolinera, string NITGasolinera, string DireccionGasolinera, string EmailGasolinera);
    }
}
