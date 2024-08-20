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
    public class rpValesPorUsuarioService: IrpValesPorUsuario
    {
        private readonly conexion conexion;
        public rpValesPorUsuarioService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<ValesModel>> ObtenerDataVales(string FechaIni, string FechaFin, int CompanyID, int PerfilUsuarioID)
        {
            IEnumerable<ValesModel> data = null;
            string sp = "EXEC SP_ValesXUsuario @FechaIni,@FechaFin,@CompanyID ,@PerfilUsuarioID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesModel>(sp, new { FechaIni, FechaFin, CompanyID,  PerfilUsuarioID }, commandType: CommandType.Text);
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

        /*public async Task<IEnumerable<ValesModel>> ObtenerDataValesDetalle(string FechaIni, string FechaFin, int CompanyID, string Username, int PerfilUsuarioID)
        {
            IEnumerable<ValesModel> data = null;
            string sp = "EXEC SP_ValesXUsuarioDetalle @FechaIni,@FechaFin,@CompanyID, @Username ,@PerfilUsuarioID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesModel>(sp, new { FechaIni, FechaFin, CompanyID, Username, PerfilUsuarioID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ValesModel>> ObtenerDataValesGeneral(string FechaIni, string FechaFin, int CompanyID, int PerfilUsuarioID)
        {
            IEnumerable<ValesModel> data = null;
            string sp = "EXEC SP_ValesXUsuarioResumen @FechaIni,@FechaFin,@CompanyID,@PerfilUsuarioID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesModel>(sp, new { FechaIni, FechaFin, CompanyID, PerfilUsuarioID }, commandType: CommandType.Text);
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
