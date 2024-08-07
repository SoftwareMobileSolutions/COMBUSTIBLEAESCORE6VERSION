﻿using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.ComponentModel.Design;
using System.Numerics;
namespace COMBUSTIBLEAESCORE.Services
{
    public class adAutorizarValesService : IadAutorizarVales
    {
        private readonly conexion conexion;

        public adAutorizarValesService(conexion _conexion)
        {
            conexion = _conexion;
        }

        public async Task<IEnumerable<mensaje>> AutorizarVale(int ValeCombustibleID, int TipoCargaID, int CentroCostoID, float? CantidadGalones, float? TotalPrecio, int ProyectoID, int UsuarioAutorizadorID)
        {
            IEnumerable<mensaje> data = null;
            string sp = "EXEC SP_AutorizarValesWEB @ValeCombustibleID, @TipoCargaID , @CentroCostoID , @CantidadGalones , @TotalPrecio , @ProyectoID , @UsuarioAutorizadorID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<mensaje>(sp, new { ValeCombustibleID, TipoCargaID , CentroCostoID , CantidadGalones , TotalPrecio , ProyectoID , UsuarioAutorizadorID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCostoAutorizalVale(string Placa, int CompanyID, int UserID)
        {
            IEnumerable<CentroCostoModel> data = null;
            string sp = "EXEC SP_ObtenerCentrosCostoAutorizalVale @Placa, @CompanyID, @UserID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<CentroCostoModel>(sp, new { Placa, CompanyID, UserID }, commandType: CommandType.Text);
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

        /*public async Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID)
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
            return data;*/

        public async Task<IEnumerable<ProyectoModel>> ObtenerProyectos(int CompanyID)
        {
            IEnumerable<ProyectoModel> data = null;
            string sp = "EXEC SP_ObtenerProyectosActivosWEB @CompanyID";
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

        public async Task<IEnumerable<ValesGeneradosModel>> ObtenerValeAutorizar(int ValeID, int CompanyID)
        {
            IEnumerable<ValesGeneradosModel> data = null;
            string sp = "EXEC SP_ObtenerValeAutorizarWEB @ValeID, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesGeneradosModel>(sp, new { ValeID, CompanyID }, commandType: CommandType.Text);
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

        public async Task<IEnumerable<ValesGeneradosModel>> ObtenerValesGenerados(string FechaIni, string FechaFin , int CompanyID)
        {
            //int? UsuarioID = null;
            IEnumerable<ValesGeneradosModel> data = null;
            string sp = "EXEC SP_ObtenerValesGeneradosWEB @FechaIni, @FechaFin, NULL, @CompanyID";
            var con = new SqlConnection(conexion.Value);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    data = await con.QueryAsync<ValesGeneradosModel>(sp, new { FechaIni,  FechaFin,  CompanyID }, commandType: CommandType.Text);
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
