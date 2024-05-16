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
    public class adAnulacionEliminacionValesService : IadAnulacionEliminacionVales
    {
        private readonly conexion conexion;

        public adAnulacionEliminacionValesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> AnularVale(int ValeCombustubibleID, int RazonID, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_AnularVale @ValeCombustubibleID,@RazonID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { ValeCombustubibleID, RazonID, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> EliminarVales(int ValeCombustubibleID, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_EliminarVales @ValeCombustubibleID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { ValeCombustubibleID, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<RazonCancelacionModel>> ObtenerRazonCancelacion(int CompanyID)
        {
            IEnumerable<RazonCancelacionModel> data = null;
            string sp = "EXEC SP_rpRazonCancelacion @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<RazonCancelacionModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<rpObtenerValesCancelarAnular>> ObtenerValesCancelarAnular(int CompanyID)
        {
            IEnumerable<rpObtenerValesCancelarAnular> data = null;
            string sp = "EXEC SP_rpObtenerValesCancelarAnular @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<rpObtenerValesCancelarAnular>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> ReestablecerVale(int ValeCombustubibleID, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ReestablecerVale @ValeCombustubibleID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { ValeCombustubibleID, CompanyID }, commandType: CommandType.Text);
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
