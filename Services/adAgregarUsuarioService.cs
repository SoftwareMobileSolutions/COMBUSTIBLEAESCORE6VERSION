using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.ComponentModel.Design;
namespace COMBUSTIBLEAESCORE.Services
{
    public class adAgregarUsuarioService : IadAgregarUsuario
    {
        private readonly conexion conexion;

        public adAgregarUsuarioService(conexion _conexion)
        {
            conexion = _conexion;
        }
        public async Task<IEnumerable<UsuarioModel>> ObtenerUsuarios(int CompanyID)
        {
            IEnumerable<UsuarioModel> data = null;
            string sp = "EXEC SP_rpUsuarios @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<UsuarioModel>(sp, new { CompanyID }, commandType: CommandType.Text);
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<PerfilModel>> ObtenerPerfilUsuarios(int CompanyID)
        {
            IEnumerable<PerfilModel> data = null;
            string sp = "EXEC SP_ObtenerPerfiles @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<PerfilModel>(sp, new { CompanyID }, commandType: CommandType.Text);
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<mensaje>> CrearUsuarios(string Nombre, string Apellido, string Username, string Clave, string Correo, int PerfilID,int GasolineraID, int ComapanyID, string Telefono)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_adCrearUsuario @Nombre, @Apellido, @Username, @Clave, @Correo, @PerfilID, @GasolineraID, @ComapanyID, @Telefono";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { Nombre, Apellido, Username, Clave, Correo, PerfilID, GasolineraID, ComapanyID, Telefono }, commandType: CommandType.Text);
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<mensaje>> ActualizarUsuario(int CompanyID, string username, string contrasena, string nombre, string apellido, string correo, string telefono)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarDatosUsuario @CompanyID, @username,  @contrasena, @nombre, @apellido, @correo, @telefono";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, username,  contrasena, nombre, apellido, correo, telefono }, commandType: CommandType.Text);
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<mensaje>> CambiarEstadoUsuario(int CompanyID, int UsuarioID, int Estado)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_CambiarEstadoUsuario @CompanyID, @UsuarioID, @Estado";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID,  UsuarioID,  Estado }, commandType: CommandType.Text);
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<GasolineraModel>> ObtenerGasolineras(int CompanyID)
        {
            IEnumerable<GasolineraModel> data = null;
            string sp = "EXEC SP_ObtenerGasolinerasCrearUsuario @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<GasolineraModel>(sp, new { CompanyID }, commandType: CommandType.Text);
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return data;
        }
    }
}
