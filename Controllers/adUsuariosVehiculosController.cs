using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Util;
using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class AdUsuariosVehiculosController : Controller
    {

        private readonly IadUsuariosVehiculos iadUsuariosVehiculos;
        public AdUsuariosVehiculosController(IadUsuariosVehiculos _iadUsuariosVehiculos)
        {
            iadUsuariosVehiculos = _iadUsuariosVehiculos;
        }

        public IActionResult AdUsuariosVehiculos()
        {
            return View();
        }  

        public async Task<JsonResult> ArbolUsuariosVehiculo() {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var dataArbol = await iadUsuariosVehiculos.ArbolUsuariosVehiculo(sesion.FirstOrDefault().CompanyID);
            return Json(dataArbol);
        }

        public async Task<JsonResult> UserXcompany()
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var usuaios = await iadUsuariosVehiculos.UserXcompany(sesion.FirstOrDefault().CompanyID);
            return Json(usuaios);
        }

        public async Task<JsonResult> MobileXUser(string Username) {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mobiles = await iadUsuariosVehiculos.mobileXUser(Username, sesion.FirstOrDefault().CompanyID);
            return Json(mobiles);
        }

        public async Task<JsonResult> obtenerObtenerMobileAsigandosXUser(string username)
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mobiles = await iadUsuariosVehiculos.obtenerObtenerMobileAsigandosXUser(username, sesion.FirstOrDefault().CompanyID);
            return Json(mobiles);

        }

        public async Task<JsonResult> ActulizarMobileXUser(string mobilesid, string Username)
        {
            //var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadUsuariosVehiculos.ActulizarMobileXUser(mobilesid, Username);
            return Json(resultado);
        }
    }
}
