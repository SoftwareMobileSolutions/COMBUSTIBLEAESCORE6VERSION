using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Linq;
namespace COMBUSTIBLEAESCORE.Services
{
    public class adRegistarCompanyService : IadRegistarCompany
    {
        public readonly conexion conexion;

        public adRegistarCompanyService(conexion _conexion)
        {
            conexion = _conexion;
        }
        public async Task<IEnumerable<mensaje>> RegisterCompany(string Nombre, string Apellido, string Username, string Clave, string Correo, string NombreCompany, string DireccionCompany)
        {

            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_adCrearCompany @Nombre,@Apellido, @Username,@Clave ,@Correo,@NombreCompany,@DireccionCompany";
            var con = new SqlConnection(conexion.Value);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { Nombre, Apellido, Username, Clave, Correo, NombreCompany, DireccionCompany }, commandType: System.Data.CommandType.Text);
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
