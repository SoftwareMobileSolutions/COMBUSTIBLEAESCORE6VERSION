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
    public class adAutorizarValesService : IadAutorizarVales
    {
        private readonly conexion conexion;

        public adAutorizarValesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID)
        {
            IEnumerable<CentroCostoModel> data = null;
            string sp = "EXEC SP_ObtenerCentrosCostoWEB @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<CentroCostoModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ProyectoModel>> ObtenerProyectos(int CompanyID)
        {
            IEnumerable<ProyectoModel> data = null;
            string sp = "EXEC SP_ObtenerProyectosWEB @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ProyectoModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ValesGeneradosModel>> ObtenerValeAutorizar(int ValeID, int CompanyID)
        {
            IEnumerable<ValesGeneradosModel> data = null;
            string sp = "EXEC SP_ObtenerValeAutorizarWEB @ValeID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesGeneradosModel>(sp, new { ValeID, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ValesGeneradosModel>> ObtenerValesGenerados(string FechaIni, string FechaFin , int CompanyID)
        {
            //int? UsuarioID = null;
            IEnumerable<ValesGeneradosModel> data = null;
            string sp = "EXEC SP_ObtenerValesGeneradosWEB @FechaIni, @FechaFin, NULL, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesGeneradosModel>(sp, new { FechaIni,  FechaFin,  CompanyID }, commandType: CommandType.Text);
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
