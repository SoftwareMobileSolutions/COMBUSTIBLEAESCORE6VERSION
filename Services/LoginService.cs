using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
namespace COMBUSTIBLEAESCORE.Services
{
    public class LoginService : ILogin
    {

        public readonly conexion conexion;

        public LoginService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<LoginModel>> login(string username, string password)
        {
            IEnumerable<LoginModel> data = null;
            string sp = "EXEC SP_Login @username, @password";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<LoginModel>(sp, new { username, password }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ModulosModel>> getModulos(string username)
        {
            IEnumerable<ModulosModel> data = null;
            string sp = "EXEC SP_ModulosXPerfil @username";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ModulosModel>(sp, new { username }, commandType: CommandType.Text);
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
