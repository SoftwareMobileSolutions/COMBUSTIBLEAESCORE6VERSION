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
    public class rpLineaTimpoValeService: IrpLineaTimpoVale
    {
        private readonly conexion conexion;

        public rpLineaTimpoValeService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<MobileXCompanyModel>> ObtenerPlacas(int CompanyID)
        {
            IEnumerable<MobileXCompanyModel> data = null;
            string sp = "EXEC SP_ObtenerMobileXCompany @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<MobileXCompanyModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<rpLineaTimpoValeModel>> ObtenerVales(int CompanyID, int MobileID, string FechaIni, string FechaFin)
        {
            IEnumerable<rpLineaTimpoValeModel> data = null;
            string sp = "EXEC SP_rpLineaTiempoVales @CompanyID, @MobileID, @FechaIni, @FechaFin ";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<rpLineaTimpoValeModel>(sp, new { CompanyID, MobileID, FechaIni, FechaFin }, commandType: CommandType.Text);
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
