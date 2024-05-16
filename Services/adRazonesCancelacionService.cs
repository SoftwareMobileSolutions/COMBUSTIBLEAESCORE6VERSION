using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Numerics;
using System.ComponentModel.Design;
namespace COMBUSTIBLEAESCORE.Services
{
    public class adRazonesCancelacionService : IadRazonesCancelacion
    {

        private readonly conexion conexion;

        public adRazonesCancelacionService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActualizarRazonCancelacion(int RazonValeCanceladoID, int CompanyID, string RazonNuevo)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarDescripcionRazonCancelacionVale @RazonValeCanceladoID,@CompanyID, @RazonNuevo";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { RazonValeCanceladoID, CompanyID, RazonNuevo }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> crearRazonCancelacion(string Razon, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_CrearRazonCancelacion @Razon,@CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { Razon, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> EliminarRazonCancelacion(int RazonValeCanceladoID, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_EliminarRazonValeCancelado @RazonValeCanceladoID,@CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { RazonValeCanceladoID, CompanyID }, commandType: CommandType.Text);
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
                    data = await con.QueryAsync<RazonCancelacionModel>(sp, new {CompanyID }, commandType: CommandType.Text);
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
