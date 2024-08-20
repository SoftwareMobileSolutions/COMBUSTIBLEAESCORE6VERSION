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
    public class rpValesPorUsuarioController : Controller
    {
        public readonly IrpValesPorUsuario irpValesPorUsuario;

        public rpValesPorUsuarioController(IrpValesPorUsuario _irpValesPorUsuario)
        {
            irpValesPorUsuario = _irpValesPorUsuario;
        }
    
        public IActionResult rpValesPorUsuario()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerDataValesGeneral(string FechaIni, string FechaFin, int PerfilUsuarioID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await irpValesPorUsuario.ObtenerDataValesGeneral(FechaIni, FechaFin, user.FirstOrDefault().CompanyID, PerfilUsuarioID);
            return Json(vales);
        }

        public async Task<JsonResult> ObtenerDataVales(string FechaIni, string FechaFin, /*string Username, */int PerfilUsuarioID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var valesDetalle = await irpValesPorUsuario.ObtenerDataVales(FechaIni, FechaFin, user.FirstOrDefault().CompanyID/*, Username*/, PerfilUsuarioID);
            return Json(valesDetalle);
        }
    }
}
