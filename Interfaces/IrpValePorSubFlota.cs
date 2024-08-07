﻿using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IrpValePorSubFlota
    {
        public Task<IEnumerable<ValeXSubFlotaModel>> ObtenerValeXSubFlota( int CompanyID, string FechaIni, string FechaFin);
        /*public Task<IEnumerable<rpTopValePorVehiculoModel>> ObtenerTopValesPorVehiculo(int NumTop, int OrderByID, int CompanyID, string FechaIni, string FechaFin);
        public Task<IEnumerable<ValeUsadoXPlacaModel>> ObtenerValesUsadosXPlaca(int CompanyID,  string Placa, string FechaIncio, string FechaFin);*/
    }
}
