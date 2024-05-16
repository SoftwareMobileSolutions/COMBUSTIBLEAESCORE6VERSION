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
    public class adCentroCostoService : IadCentroCosto
    {
        private readonly conexion conexion;

        public adCentroCostoService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActualizarCentrosCosto(int CentroCostoID,int CompanyID, string NombreNuevo)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarNombreCentroCosto @CentroCostoID, @CompanyID, @NombreNuevo";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CentroCostoID, CompanyID, NombreNuevo }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> crearCentroCosto(string Nombre, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_CrearCentroCosto @Nombre,@CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { Nombre, CompanyID}, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> EliminarCentroCosto(int CentroCostoID, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_EliminarCentroCosto @CentroCostoID,@CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CentroCostoID, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID)
        {
            IEnumerable<CentroCostoModel> data = null;
            string sp = "EXEC SP_rpCentroCosto @CompanyID";
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
    }
}
