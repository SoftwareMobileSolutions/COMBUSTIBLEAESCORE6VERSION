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
    public class adUsuariosVehiculosService : IadUsuariosVehiculos
    {
        private readonly conexion conexion;

        public adUsuariosVehiculosService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActulizarMobileXUser(string mobilesid, string Username)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActulizarMobileXUser @mobilesid, @Username";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { mobilesid, Username }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ArbolUsuariosVehiculoModel>> ArbolUsuariosVehiculo(int CompanyID)
        {
            IEnumerable<ArbolUsuariosVehiculoModel> data = null;
            string sp = "EXEC adArbolUsuarioVehiculo @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ArbolUsuariosVehiculoModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<MobileXUserModel>> mobileXUser(string Username, int CompanyID)
        {
            IEnumerable<MobileXUserModel> data = null;
            string sp = "EXEC SP_MobileXUser @Username, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<MobileXUserModel>(sp, new { Username, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<MobileAsigandosXUserModel>> obtenerObtenerMobileAsigandosXUser(string username, int CompanyID)
        {
            IEnumerable<MobileAsigandosXUserModel> data = null;
            string sp = "EXEC SP_ObtenerMobileAsigandosXUser @username, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<MobileAsigandosXUserModel>(sp, new { username, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<UserXcompanyModel>> UserXcompany(int CompanyID)
        {
            IEnumerable<UserXcompanyModel> data = null;
            string sp = "EXEC SP_adUserXcompany @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<UserXcompanyModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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
