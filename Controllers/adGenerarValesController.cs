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
    public class adGenerarValesController : Controller
    {
        private readonly IadGenerarVales iadGenerarVales;
        public adGenerarValesController(IadGenerarVales _IadGenerarVales)
        {
            iadGenerarVales = _IadGenerarVales;
        }

        public IActionResult adGenerarVales()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerMobileXUser()//Action para obtener los vales por usuario
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mobiles = await iadGenerarVales.ObtenerMobileXUser(user.FirstOrDefault().UsuarioID);
            return Json(mobiles); 
        }

        public async Task<JsonResult> ObtenerTipoCarga()//Action para obtener los tipos de carga
        {
            var tiposCarga = await iadGenerarVales.ObtenerTipoCarga();
            return Json(tiposCarga);
        }

        public async Task<JsonResult> ObtenerValesGenerados(string FechaIni, string FechaFin)//Action para obtener los vales generados
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var dataUser = sesion.FirstOrDefault();
            var vales = await iadGenerarVales.ObtenerValesGenerados(FechaIni, FechaFin, dataUser.UsuarioID, dataUser.CompanyID);
            return Json(vales);
        }

        [HttpPost]
        //Action para generar los vales
        public async Task<JsonResult> GenerarVale(int MobileID, string FechaGeneracion, int TipoCargaValeID, float? TotalDolares, float? TotaGalones)
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var dataUser = sesion.FirstOrDefault();
            var mensaje = await iadGenerarVales.GenerarVale(MobileID, dataUser.CompanyID, FechaGeneracion,dataUser.UsuarioID, TipoCargaValeID, TotalDolares, TotaGalones);
            return Json(mensaje);
        }
    }
}
