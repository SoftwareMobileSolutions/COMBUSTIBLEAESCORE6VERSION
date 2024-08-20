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
        {   // Se obtiene la conexión
            conexion = _conexion;
        }

        public async Task<IEnumerable<LoginModel>> login(string username, string password)
        {   
            IEnumerable<LoginModel> data = null;
            string sp = "EXEC SP_Login @username, @password";
            
            // se inicializa la conexion 
            var con = new SqlConnection(conexion.Value);
            try
            {   //Se verifica si la conexion esta cerrada 
                if (con.State == ConnectionState.Closed)
                {   
                    //Se abre la conexión 
                    con.Open();
                    //Se ejecuta el SP en el server 
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

        public async Task<IEnumerable<ModulosModel>> getModulos(string username, int CompanyID)
        {
            IEnumerable<ModulosModel> data = null;
            string sp = "EXEC SP_ModulosXPerfil @username, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ModulosModel>(sp, new { username, CompanyID }, commandType: CommandType.Text);
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
