using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adAutorizarValesController : Controller
    {
        private readonly IadAutorizarVales iadAutorizarVales;
        public adAutorizarValesController(IadAutorizarVales _IadAutorizarVales)
        {
            iadAutorizarVales = _IadAutorizarVales;
        }
        public IActionResult adAutorizarVales()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerValesGenerados(string FechaIni, string FechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await iadAutorizarVales.ObtenerValesGenerados(FechaIni, FechaFin, user.FirstOrDefault().CompanyID);
            return Json(vales);

        }

        public async Task<JsonResult> ObtenerCentrosCostoAutorizalVale(string Placa)
        {
            var Sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var data = Sesion.FirstOrDefault();
            var CentrosCosto = await iadAutorizarVales.ObtenerCentrosCostoAutorizalVale(Placa, data.CompanyID, data.UsuarioID);
            return Json(CentrosCosto);
        } 
        public async Task<JsonResult> ObtenerProyectos()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var proyectos = await iadAutorizarVales.ObtenerProyectos(user.FirstOrDefault().CompanyID);
            return Json(proyectos);
        }

        public async Task<JsonResult> ObtenerValeAutorizar(int ValeID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var data = await iadAutorizarVales.ObtenerValeAutorizar(ValeID, user.FirstOrDefault().CompanyID);
            return Json(new { valeAutorizar = data.Item1, valeAnterior = data.Item2, mensaje = data.Item3});
        }


        [HttpPost]
        public async Task<JsonResult> AutorizarVale(int ValeCombustibleID, int TipoCargaID, int CentroCostoID, float? CantidadGalones, float? TotalPrecio, int ProyectoID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mensaje = await iadAutorizarVales.AutorizarVale(ValeCombustibleID, TipoCargaID, CentroCostoID, CantidadGalones, TotalPrecio, ProyectoID, user.FirstOrDefault().UsuarioID);
            return Json(mensaje);
        }

        public async Task<JsonResult> ObtenerRazonCancelacion()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var razonesCancel = await iadAutorizarVales.ObtenerRazonCancelacion(user.FirstOrDefault().CompanyID);
            return Json(razonesCancel);
        }

        [HttpPost]
        public async Task<JsonResult> AnularVale(int ValeCombustubibleID, int RazonID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAutorizarVales.AnularVale(ValeCombustubibleID, user.FirstOrDefault().UsuarioID, RazonID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerValesXEstado(int EstadoValeID, string FechaIni, string FechaFin)
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var dataUser = sesion.FirstOrDefault();
            var vales = await iadAutorizarVales.ObtenerValesXEstado(dataUser.UsuarioID, EstadoValeID, FechaIni, FechaFin, dataUser.CompanyID);
            return Json(vales);
        }
    }
}
