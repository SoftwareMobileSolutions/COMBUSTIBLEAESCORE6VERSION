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
    public class adAsignacionCentrosCostoService: IadAsignacionCentrosCosto
    {
        private readonly conexion conexion;

        public adAsignacionCentrosCostoService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> ActualizarCentroCostoXMobile(int MobileID, int CentroCostoID, int UserAsignaID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarCentroCostoXMobile @MobileID, @CentroCostoID, @UserAsignaID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { MobileID, CentroCostoID, UserAsignaID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<mensaje>> ActualizarCentroCostoXUser(int UsuarioID, string CentrosCostoID, int UserAsignaID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_ActualizarCentroCostoXUser @UsuarioID, @CentrosCostoID, @UserAsignaID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { UsuarioID, CentrosCostoID, UserAsignaID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<CentroCostoXMobileModel>> ObtenerCentroCostoXMobile(int CompanyID)
        {
            IEnumerable<CentroCostoXMobileModel> data = null;
            string sp = "EXEC SP_ObtenerCentroCostoXMobile @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<CentroCostoXMobileModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<CentroCostoXUserModel>> ObtenerCentroCostoXUser(int CompanyID, int UsuarioID)
        {
            IEnumerable<CentroCostoXUserModel> data = null;
            string sp = "EXEC SP_ObtenerCentroCostoXUser @UsuarioID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<CentroCostoXUserModel>(sp, new { CompanyID, UsuarioID }, commandType: CommandType.Text);
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
            string sp = "EXEC SP_ObtenerCentrosCostoWEB @CompanyID";
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

        public async Task<IEnumerable<UsuarioModel>> ObtenerUsuarios(int CompanyID)
        {
            IEnumerable<UsuarioModel> data = null;
            string sp = "EXEC SP_ObtenerUsuariosXCompanyXPerfil @CompanyID, 3";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<UsuarioModel>(sp, new { CompanyID }, commandType: CommandType.Text);
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
