using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class rpValesCombustibleController : Controller
    {
        IrpVales IrpVales;
        public rpValesCombustibleController(IrpVales _IrpVales)
        {
            IrpVales = _IrpVales;
        }
        public async Task<IActionResult> rpValesCombustible()
        {
            return await Task.Run(() => {
                return PartialView();
            });
        }

        [HttpGet]
        public async Task<JsonResult> getDataValesCombustible(string fini, string ffin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var data = await IrpVales.getRpValesCombustible(user.FirstOrDefault().UsuarioID, user.FirstOrDefault().CompanyID, fini, ffin);
            return Json(data);
        }
    }
}
