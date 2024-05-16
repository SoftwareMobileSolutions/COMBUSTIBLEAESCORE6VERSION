using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAgregarUsuario
    {
        public Task<IEnumerable<UsuarioModel>> ObtenerUsuarios(int CompanyID);        
        public Task<IEnumerable<PerfilModel>> ObtenerPerfilUsuarios();
        public Task<IEnumerable<GasolineraModel>> ObtenerGasolineras(int CompanyID);
        public Task<IEnumerable<mensaje>> CrearUsuarios(string Nombre, string Apellido, string Username, string Clave, string Correo, int PerfilID,int GasolineraID, int ComapanyID, string Telefono);
        public Task<IEnumerable<mensaje>> ActualizarUsuario(int CompanyID, string username, string contrasena, string nombre, string apellido, string correo, string telefono);
        public Task<IEnumerable<mensaje>> CambiarEstadoUsuario(int CompanyID, int UsuarioID, int Estado);
    }
}
