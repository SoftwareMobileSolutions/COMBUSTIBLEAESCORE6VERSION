using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Reflection;
using System.ComponentModel.Design;
namespace COMBUSTIBLEAESCORE.Services
{
    public class adVehiculosFlotasService : Interfaces.IadVehiculosFlotas
    {

        private readonly conexion conexion;

        public adVehiculosFlotasService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> agregarVehiculo(int CompanyID, string PlacaNew, string NombreNew, string Marca, string Modelo, int FlotaID, float KmXGalon, float CapacidadTanque, int TipoCombustibleID, string VINNew)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_adAgregarVehiculo @CompanyID,@PlacaNew,@NombreNew,@Marca,@Modelo,@FlotaID,@KmXGalon,@CapacidadTanque,@TipoCombustibleID,@VINNew";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, PlacaNew, NombreNew, Marca, Modelo, FlotaID, KmXGalon, CapacidadTanque, TipoCombustibleID, VINNew }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> agregarFlota(int CompanyID, string SubfleetName)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_adAgregarFlota @CompanyID, @SubfleetName";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, SubfleetName }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<FlotaModel>> obtenerFlotas(int CompanyID)
        {
            IEnumerable<FlotaModel> data = null;
            string sp = "EXEC SP_ObtenerInformacionFLotas @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<FlotaModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<rpVehiculoModel>> rpVehiculo(int CompanyID, string FechaIni, string FechaFin)
        {
            IEnumerable<rpVehiculoModel> data = null;
            string sp = "EXEC SP_rpMobileXCompany  @FechaIni, @FechaFin, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<rpVehiculoModel>(sp, new {  FechaIni, FechaFin , CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<CombustibleMobileModel>> obtenerTipoCombustible()
        {
            IEnumerable<CombustibleMobileModel> data = null;
            string sp = "EXEC SP_ObtenerCombustibleMobile";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<CombustibleMobileModel>(sp, new { }, commandType: CommandType.Text);
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
