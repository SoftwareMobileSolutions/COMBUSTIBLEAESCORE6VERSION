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
    public class adGasolineraService: IadGasolinera
    {
        private readonly conexion conexion;

        public adGasolineraService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> CrearGasolinera(int CompanyID, string DescriEstacionServicio, float Latitud, float Longitud)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_CrearGasolinera @CompanyID, @DescriEstacionServicio, @Latitud, @Longitud";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, DescriEstacionServicio, Latitud, Longitud }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<GasolineraModel>> ObtenerGasolineras(int CompanyID)
        {
            IEnumerable<GasolineraModel> data = null;
            string sp = "EXEC SP_rpGasolinera @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<GasolineraModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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
