﻿using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadAsignacionCentrosCosto
    {
        public Task<IEnumerable<CentroCostoModel>> ObtenerCentrosCosto(int CompanyID);
        public Task<IEnumerable<UsuarioModel>> ObtenerUsuarios(int CompanyID);
        public Task<IEnumerable<CentroCostoXUserModel>> ObtenerCentroCostoXUser(int CompanyID, int UsuarioID);
        public Task<IEnumerable<mensaje>> ActualizarCentroCostoXUser(int UsuarioID, string CentrosCostoID, int UserAsignaID);
        public Task<IEnumerable<CentroCostoXMobileModel>> ObtenerCentroCostoXMobile(int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarCentroCostoXMobile(int MobileID, int CentroCostoID, int UserAsignaID);
    }
}
