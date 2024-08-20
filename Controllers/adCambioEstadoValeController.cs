﻿using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adCambioEstadoValeController : Controller
    {
        private readonly IadCambioEstadoVale iadCambioEstadoVale;

        public adCambioEstadoValeController(IadCambioEstadoVale _IadCambioEstadoVale)
        {
            iadCambioEstadoVale = _IadCambioEstadoVale;
        }
        public IActionResult adCambioEstadoVale()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerMobilesXCompany()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Mobiles = await iadCambioEstadoVale.ObtenerMobilesXCompany(user.FirstOrDefault().CompanyID);
            return Json(Mobiles);
        }

        public async Task<JsonResult> ObtenerUsuarioXCompany()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var usuarios = await iadCambioEstadoVale.ObtenerUsuarioXCompany(user.FirstOrDefault().CompanyID);   
            return Json(usuarios);
        }
         
        public async Task<JsonResult> ObtenerVales(int FiltroID, string NumVale, int? MobileID, string FechaIni, string FechaFin, int? UsuarioID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await iadCambioEstadoVale.ObtenerVales(user.FirstOrDefault().CompanyID, FiltroID, NumVale, MobileID, FechaIni, FechaFin, UsuarioID);    
            return Json(vales);
        }

        public async Task<JsonResult> ObtenerEstados()
        {
            var estados = await iadCambioEstadoVale.ObtenerEstadosVale();
            return Json(estados);
        }
        [HttpPost]
        public async Task<JsonResult> CambioEstadoVales(string ValesID, int EstadoID, string CreditoFiscal, string FechaCreditoFiscal) {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadCambioEstadoVale.CambiarEstadoVales(ValesID,  EstadoID, user.FirstOrDefault().UsuarioID,  CreditoFiscal,  FechaCreditoFiscal);
            return Json(resultado);
        }
    }
}