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
    public class adCambioEstadoValeService:IadCambioEstadoVale
    {
        private readonly conexion conexion;

        public adCambioEstadoValeService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<EstadoValeModel>> ObtenerEstadosVale()
        {
            IEnumerable<EstadoValeModel> data = null;
            string sp = "EXEC SP_ObtenerEstadoValeCambiarEstado";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<EstadoValeModel>(sp, new {  }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<MobileXCompanyModel>> ObtenerMobilesXCompany(int CompanyID)
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

        public async Task<IEnumerable<UsuarioModel>> ObtenerUsuarioXCompany(int CompanyID)
        {
            IEnumerable<UsuarioModel> data = null;
            string sp = "EXEC SP_ObtenerUsuariosXCompany @CompanyID";
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

        public async Task<IEnumerable<ValesModel>> ObtenerVales(int CompanyID, int FiltroID, string NumVale, int? MobileID, string FechaIni, string FechaFin, int? UsuarioID)
        {
            IEnumerable<ValesModel> data = null;
            string sp = "EXEC SP_ObtenerValesCambioEstado @CompanyID, @FiltroID, @NumVale, @MobileID, @FechaIni, @FechaFin, @UsuarioID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesModel>(sp, new { CompanyID, FiltroID, NumVale, MobileID, FechaIni,  FechaFin, UsuarioID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> CambiarEstadoVales(string ValesID, int EstadoID, int UsuarioID, string CreditoFiscal, string FechaCreditoFiscal)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarEstadoVales @ValesID, @EstadoID, @UsuarioID, @CreditoFiscal, @FechaCreditoFiscal";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new {  ValesID,  EstadoID,  UsuarioID, CreditoFiscal, FechaCreditoFiscal }, commandType: CommandType.Text);
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
