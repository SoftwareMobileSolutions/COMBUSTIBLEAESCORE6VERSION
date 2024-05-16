using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adRazonesCancelacionController : Controller
    {
        private readonly IadRazonesCancelacion iadRazonesCancelacion;

        public adRazonesCancelacionController(IadRazonesCancelacion _iadRazonesCancelacion)
        {
            iadRazonesCancelacion = _iadRazonesCancelacion;
        }

        public IActionResult adRazonesCancelacion()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> crearRazonCancelacion(string Razon)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadRazonesCancelacion.crearRazonCancelacion(Razon, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerRazonCancelacion()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var razones = await iadRazonesCancelacion.ObtenerRazonCancelacion(user.FirstOrDefault().CompanyID);
            return Json(razones);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarRazonCancelacion(int RazonValeCanceladoID, string RazonNuevo)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadRazonesCancelacion.ActualizarRazonCancelacion(RazonValeCanceladoID, user.FirstOrDefault().CompanyID, RazonNuevo);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarRazonCancelacion(int RazonValeCanceladoID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadRazonesCancelacion.EliminarRazonCancelacion(RazonValeCanceladoID,user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }
    }
}
