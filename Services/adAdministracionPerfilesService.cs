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
    public class adAdministracionPerfilesService:IadAdministracionPerfiles
    {
        private readonly conexion conexion;

        public adAdministracionPerfilesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActualizarPerfilXModulos(int CompanyID, int PerfilID, string NombrePerfil_Nuevo, string ModulosID)
        {
            //throw new System.NotImplementedException();
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarPerfilXCompany @CompanyID, @PerfilID, @NombrePerfil_Nuevo, @ModulosID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, PerfilID, NombrePerfil_Nuevo, ModulosID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> CrearPerfil(string NombrePefil, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_CrearPerfil @NombrePefil, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { NombrePefil, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ModulosAsignadosModel>> ObtenerModulos(int PerfilID, int CompanyID)
        {
            IEnumerable<ModulosAsignadosModel> data = null;
            string sp = "EXEC SP_ObtenerModulosAsignadosXperfil @PerfilID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ModulosAsignadosModel>(sp, new { PerfilID ,CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<PerfilModel>> ObtenerPefiles(int CompanyID)
        {
            IEnumerable<PerfilModel> data = null;
            string sp = "EXEC SP_ObtenerPerfiles @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<PerfilModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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
