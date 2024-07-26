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
    public class rpValesGasolineraService : IrpValesGasolinera
    {
        private readonly conexion conexion;

        public rpValesGasolineraService(conexion _conexion)
        {
            conexion = _conexion;
        }
        public async Task<IEnumerable<ValesXGasolineraModel>> ObtenerValesXGasolinera(int CompanyID, string FechaIni, string FechaFin)
        {
            IEnumerable<ValesXGasolineraModel> data = null;
            string sp = "EXEC SP_ObtenerValesPorGasolinera @CompanyID, @FechaIni, @FechaFin";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesXGasolineraModel>(sp, new { CompanyID, FechaIni, FechaFin }, commandType: CommandType.Text);
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
