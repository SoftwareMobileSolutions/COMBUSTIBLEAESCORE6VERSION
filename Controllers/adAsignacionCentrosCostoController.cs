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

        public async Task<JsonResult> ObtenerCentrosCosto()//Action para obtener centros de costo
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentrosCosto = await iadAsignacionCentrosCosto.ObtenerCentrosCosto(user.FirstOrDefault().CompanyID);
            return Json(CentrosCosto);
        }

        public async Task<JsonResult> ObtenerUsuarios()//Action sin uso
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var usuarios = await iadAsignacionCentrosCosto.ObtenerUsuarios(user.FirstOrDefault().CompanyID);
            return Json(usuarios);
        }

        public  async Task<JsonResult> ObtenerCentroCostoXUser(int UsuarioID)//Action sin uso
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentroCostosXUser = await iadAsignacionCentrosCosto.ObtenerCentroCostoXUser(user.FirstOrDefault().CompanyID,UsuarioID);
            return Json(CentroCostosXUser);
        }
        [HttpPost]
        public  async Task<JsonResult> ActualizarCentroCostoXUser(int UsuarioID, string CentrosCostoID)//Action si uso
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mensaje = await iadAsignacionCentrosCosto.ActualizarCentroCostoXUser(UsuarioID, CentrosCostoID, user.FirstOrDefault().UsuarioID);
            return Json(mensaje);
        }

        public async Task<JsonResult> ObtenerCentroCostoXMobile()//Action para obtener centros de costo por mobile
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentroCostoXMobile = await iadAsignacionCentrosCosto.ObtenerCentroCostoXMobile(user.FirstOrDefault().CompanyID);
            return Json(CentroCostoXMobile);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarCentroCostoXMobile(int MobileID, int CentroCostoID)//Action para actualizar el centro de costo 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mensaje = await iadAsignacionCentrosCosto.ActualizarCentroCostoXMobile(MobileID, CentroCostoID, user.FirstOrDefault().CompanyID);
            return Json(mensaje);
        }
    }
}
