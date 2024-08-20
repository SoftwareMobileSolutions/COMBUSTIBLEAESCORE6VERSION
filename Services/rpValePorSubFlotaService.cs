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
    public class rpValePorSubFlotaService : IrpValePorSubFlota
    {
        private readonly conexion conexion;

        public rpValePorSubFlotaService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<ValeXSubFlotaModel>> ObtenerValeXSubFlota(int CompanyID, string FechaIni, string FechaFin)
        {
            IEnumerable<ValeXSubFlotaModel> data = null;
            string sp = "EXEC SP_ObtenerValesXSubFlota @CompanyID, @FechaIni, @FechaFin";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValeXSubFlotaModel>(sp, new { CompanyID, FechaIni, FechaFin }, commandType: CommandType.Text);
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
        /*public async Task<IEnumerable<rpTopValePorVehiculoModel>> ObtenerTopValesPorVehiculo(int NumTop, int OrderByID, int CompanyID, string FechaIni, string FechaFin)
{
   IEnumerable<rpTopValePorVehiculoModel> data = null;
   string sp = "EXEC SP_TopValesXVehiculo @NumTop,@OrderByID, @CompanyID, @FechaIni, @FechaFin";
   var con = new SqlConnection(conexion.Value);
   try
   {
       if (con.State == ConnectionState.Closed)
       {
           con.Open();
           data = await con.QueryAsync<rpTopValePorVehiculoModel>(sp, new { NumTop, OrderByID, CompanyID, FechaIni, FechaFin }, commandType: CommandType.Text);
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

public async Task<IEnumerable<ValeUsadoXPlacaModel>> ObtenerValesUsadosXPlaca(int CompanyID, string Placa, string FechaIncio, string FechaFin)
{
   IEnumerable<ValeUsadoXPlacaModel> data = null;
   string sp = "EXEC SP_ObtenerValesUsadosXPlaca @CompanyID, @Placa, @FechaIncio, @FechaFin";
   var con = new SqlConnection(conexion.Value);
   try
   {
       if (con.State == ConnectionState.Closed)
       {
           con.Open();
           data = await con.QueryAsync<ValeUsadoXPlacaModel>(sp, new { CompanyID, Placa, FechaIncio, FechaFin }, commandType: CommandType.Text);
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
    }
}
