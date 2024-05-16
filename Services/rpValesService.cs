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
    public class rpValesService : IrpVales
    {
        private readonly conexion conexion;

        public rpValesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<rpValesCombustibleModel>> getRpValesCombustible(int userid, int companyid, string fini, string ffin)
        {
            IEnumerable<rpValesCombustibleModel> datos = null;
            string comando = "EXEC SP_rpVales @fini, @ffin, @userid, @companyid";
            var conn = new SqlConnection(conexion.Value);
            /* try
             {*/
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                datos = await conn.QueryAsync<rpValesCombustibleModel>(comando, new { fini, ffin, userid, companyid}, commandType: CommandType.Text);
            }
            /* }
             finally
             {*/
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            // }
            return datos;
        }
    }
}
