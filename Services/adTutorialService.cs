using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.ComponentModel.Design;
using ImageMagick;
namespace COMBUSTIBLEAESCORE.Services
{
    public class adTutorialService: IadTutorial
    {
        private readonly conexion conexion;

        public adTutorialService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async  Task<IEnumerable<mensaje>> ValidarPaso(int CompanyID, int Paso)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ValidacionTutorial @CompanyID, @Paso";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID , Paso }, commandType: CommandType.Text);
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
