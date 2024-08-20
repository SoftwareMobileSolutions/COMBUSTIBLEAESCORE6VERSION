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
    public class adLiquidarValesService : IadLiquidarVales
    {
        private readonly conexion conexion;

        public adLiquidarValesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> LiquidarVale(string placa, int userID, int CompanyID, double CantidadGalones, double TotalDolares, int Odometro)
        {
            IEnumerable<mensaje> data = null;

            string sp = "EXEC SP_LiquidarVale @placa, @userID, @CompanyID, @CantidadGalones, @TotalDolares, @Odometro;";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { placa, userID, CompanyID, CantidadGalones, TotalDolares, Odometro }, commandType: CommandType.Text);
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
        public async Task<IEnumerable<adGetCerrarValesModel>> getInformationVale(int CompanyID, string placa)
        {
            IEnumerable<adGetCerrarValesModel> data = null;
            string sp = "EXEC SP_ObtenerInformacionCerrarVale @CompanyID, @placa;";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<adGetCerrarValesModel>(sp, new { CompanyID, placa }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<rpValesCerradosModel>> ObtenerValesLiquidados(int UserID, int CompanyID, string FechaIncio, string fechaFin)
        {
            IEnumerable<rpValesCerradosModel> data = null;
            string sp = "EXEC SP_ObtenerValesLiquidados @UserID,@CompanyID,@FechaIncio,@fechaFin";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<rpValesCerradosModel>(sp, new { UserID, CompanyID, FechaIncio, fechaFin }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<rpValesCerradosXPlacaModel>> ObtenerValesLiquidadosXPlaca(int UserID, int CompanyID, string Placa, string FechaIncio, string fechaFin)
        {
            IEnumerable<rpValesCerradosXPlacaModel> data = null;
            string sp = "EXEC SP_ObtenerValesLiquidadosXPlaca @UserID,@CompanyID,@Placa,@FechaIncio,@fechaFin";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<rpValesCerradosXPlacaModel>(sp, new { UserID, CompanyID, Placa, FechaIncio, fechaFin }, commandType: CommandType.Text);
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
