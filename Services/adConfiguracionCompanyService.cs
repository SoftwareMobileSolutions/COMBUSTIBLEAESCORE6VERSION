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
    public class adConfiguracionCompanyService : IadConfiguracionCompany
    {
        private readonly conexion conexion;

        public adConfiguracionCompanyService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActualizarInformacion(int CompanyID, string NombreCompany, string Direccion, string SitioWeb, string Correo, string RazonSocial, int? DepartamentoID, int? MunicipioID, string Descripcion, string TelMovil, string Telfijo)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActulizarDataCompany @CompanyID, @NombreCompany, @Direccion, @SitioWeb, @Correo, @RazonSocial, @DepartamentoID, @MunicipioID, @Descripcion, @TelMovil, @Telfijo";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { CompanyID, NombreCompany, Direccion, SitioWeb, Correo, RazonSocial, DepartamentoID, MunicipioID, Descripcion, TelMovil, Telfijo }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<CompanyModel>> ObtenerDataCompany(int CompanyID)
        {
            IEnumerable<CompanyModel> data = null;
            string sp = "EXEC SP_ObtenerDataCompany @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<CompanyModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<DepartamentoModel>> ObtenerDepartamentos()
        {
            IEnumerable<DepartamentoModel> data = null;
            string sp = "EXEC SP_ObtenerDepartamentos";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<DepartamentoModel>(sp, new {}, commandType: CommandType.Text);
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

        public async Task<IEnumerable<MunicipioModel>> ObtenerMunicipios(int DepartamentoID)
        {
            IEnumerable<MunicipioModel> data = null;
            string sp = "EXEC SP_ObtenerMunicipios @DepartamentoID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<MunicipioModel>(sp, new { DepartamentoID }, commandType: CommandType.Text);
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
