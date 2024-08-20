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
    public class adGenerarValesService : IadGenerarVales
    {
        private readonly conexion conexion;

        public adGenerarValesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> GenerarVale(int MobileID, int CompanyID, string FechaGeneracion, int UserID, int TipoCargaValeID, float? TotalDolares, float? TotaGalones)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_GenerarValeWEB @MobileID, @CompanyID, @FechaGeneracion, @UserID, @TipoCargaValeID, @TotalDolares, @TotaGalones";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { MobileID, CompanyID, FechaGeneracion, UserID, TipoCargaValeID, TotalDolares, TotaGalones }, commandType: CommandType.Text);
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

        /*public async Task<IEnumerable<MobileXCompanyModel>> ObtenerMobileXCompany(int CompanyID)
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
        }*/

        public async Task<IEnumerable<MobileAsigandosXUserModel>> ObtenerMobileXUser(int UserID)
        {
            IEnumerable<MobileAsigandosXUserModel> data = null;
            string sp = "EXEC SP_ObtenerMobileXUserGenerarVales @UserID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<MobileAsigandosXUserModel>(sp, new { UserID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<TipoCargaModel>> ObtenerTipoCarga()
        {
            IEnumerable<TipoCargaModel> data = null;
            string sp = "EXEC SP_ObtenerTipoCarga";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<TipoCargaModel>(sp, new {  }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ValesGeneradosModel>> ObtenerValesGenerados(string FechaIni, string FechaFin, int UsuarioID, int CompanyID)
        {
            IEnumerable<ValesGeneradosModel> data = null;
            string sp = "EXEC SP_ObtenerValesGeneradosWEB @FechaIni, @FechaFin, @UsuarioID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesGeneradosModel>(sp, new { FechaIni, FechaFin, UsuarioID, CompanyID }, commandType: CommandType.Text);
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
