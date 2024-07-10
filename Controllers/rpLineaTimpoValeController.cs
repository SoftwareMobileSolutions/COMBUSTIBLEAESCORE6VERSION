using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class rpLineaTimpoValeController : Controller
    {
        public readonly IrpLineaTimpoVale irpLineaTimpoVale;

        public rpLineaTimpoValeController(IrpLineaTimpoVale _IrpLineaTimpoVale)
        {
            irpLineaTimpoVale = _IrpLineaTimpoVale;
        }
        public IActionResult rpLineaTimpoVale()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerVales(int MobileID, string FechaIni, string FechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await irpLineaTimpoVale.ObtenerVales(user.FirstOrDefault().CompanyID, MobileID, FechaIni, FechaFin);
            return Json(vales);
        }

        public async Task<JsonResult> ObtenerPlacas()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mobiles = await irpLineaTimpoVale.ObtenerPlacas(user.FirstOrDefault().CompanyID);   
            return Json(mobiles);
        }
    }
}
