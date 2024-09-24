using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Reflection;

namespace COMBUSTIBLEAESCORE.Services
{
    public class adProyectoService : IadProyecto
    {
        private readonly conexion conexion;

        public adProyectoService(conexion _conexion) {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActualizarProyecto(int CompanyID, int ProyectoID, string Responsable, int Estado, int Bandera)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarProyecto @CompanyID, @ProyectoID, @Responsable, @Estado, @Bandera";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, ProyectoID , Responsable , Estado , Bandera }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> EliminarProyecto(int ProyectoID, int CompanyID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_EliminarProyecto @ProyectoID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { ProyectoID, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> InsertarProyectos(string CodigoProyecto_NombreProyecto, string Responsable_Estado, int CompanyID, int UsuarioID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_adInsertarProyectos @CodigoProyecto_NombreProyecto, @Responsable_Estado, @CompanyID, @UsuarioID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CodigoProyecto_NombreProyecto, Responsable_Estado, CompanyID, UsuarioID }, commandType: CommandType.Text);
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
    }
}