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
    public class adCentroCostoController : Controller
    {
        private readonly IadCentroCosto iadCentroCosto;

        public adCentroCostoController(IadCentroCosto _IadCentroCosto)
        {
            iadCentroCosto = _IadCentroCosto;
        }
        public IActionResult adCentroCosto()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> crearCentroCosto(string Nombre) {

            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadCentroCosto.crearCentroCosto(Nombre, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerCentrosCosto()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentrosCosto = await iadCentroCosto.ObtenerCentrosCosto(user.FirstOrDefault().CompanyID);
            return Json(CentrosCosto);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarCentrosCosto(int CentroCostoID, string NombreNuevo)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadCentroCosto.ActualizarCentrosCosto(CentroCostoID, user.FirstOrDefault().CompanyID, NombreNuevo);
            return Json(resultado);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarCentroCosto(int CentroCostoID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadCentroCosto.EliminarCentroCosto(CentroCostoID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }
    }
}
