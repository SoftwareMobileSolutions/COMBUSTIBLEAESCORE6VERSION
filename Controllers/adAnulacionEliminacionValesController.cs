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
    public class adAnulacionEliminacionValesController : Controller
    {
        private readonly IadAnulacionEliminacionVales iadAnulacionEliminacionVales;

        public adAnulacionEliminacionValesController(IadAnulacionEliminacionVales _IadAnulacionEliminacionVales)
        {
            iadAnulacionEliminacionVales = _IadAnulacionEliminacionVales;
        }
        public IActionResult adAnulacionEliminacionVales()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerValesCancelarAnular()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await iadAnulacionEliminacionVales.ObtenerValesCancelarAnular(user.FirstOrDefault().CompanyID);
            return Json(vales);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarVales(int ValeCombustubibleID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAnulacionEliminacionVales.EliminarVales(ValeCombustubibleID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerRazonCancelacion()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var razones = await iadAnulacionEliminacionVales.ObtenerRazonCancelacion(user.First().CompanyID);
            return Json(razones);
        }
        [HttpPost]
        public async Task<JsonResult> AnularVale(int ValeCombustubibleID, int RazonID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAnulacionEliminacionVales.AnularVale(ValeCombustubibleID, RazonID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> ReestablecerVale(int ValeCombustubibleID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAnulacionEliminacionVales.ReestablecerVale(ValeCombustubibleID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }
    }
}
