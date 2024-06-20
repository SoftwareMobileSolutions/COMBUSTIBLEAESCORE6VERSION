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
    public class adAsignacionCentrosCostoController : Controller
    {

        private readonly IadAsignacionCentrosCosto iadAsignacionCentrosCosto;

        public adAsignacionCentrosCostoController(IadAsignacionCentrosCosto _IadAsignacionCentrosCosto)
        {
            iadAsignacionCentrosCosto = _IadAsignacionCentrosCosto;
        }
        public IActionResult adAsignacionCentrosCosto()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerCentrosCosto()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentrosCosto = await iadAsignacionCentrosCosto.ObtenerCentrosCosto(user.FirstOrDefault().CompanyID);
            return Json(CentrosCosto);
        }

        public async Task<JsonResult> ObtenerUsuarios()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var usuarios = await iadAsignacionCentrosCosto.ObtenerUsuarios(user.FirstOrDefault().CompanyID);
            return Json(usuarios);
        }

        public  async Task<JsonResult> ObtenerCentroCostoXUser(int UsuarioID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentroCostosXUser = await iadAsignacionCentrosCosto.ObtenerCentroCostoXUser(user.FirstOrDefault().CompanyID,UsuarioID);
            return Json(CentroCostosXUser);
        }
    }
}
