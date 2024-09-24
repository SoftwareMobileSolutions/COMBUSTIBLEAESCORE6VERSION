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

        public async Task<JsonResult> ArbolUsuariosVehiculo() {//Action para arbol
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var dataArbol = await iadUsuariosVehiculos.ArbolUsuariosVehiculo(sesion.FirstOrDefault().CompanyID);
            return Json(dataArbol);
        }

        public async Task<JsonResult> UserXcompany()//Action para obtener los usuarios por compañia
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion
            var usuaios = await iadUsuariosVehiculos.UserXcompany(sesion.FirstOrDefault().CompanyID);//Se obtienen los usuarios
            return Json(usuaios);
        }

        public async Task<JsonResult> MobileXUser(string Username) {//Action para obtener los vehiculos por usuario
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion
            var mobiles = await iadUsuariosVehiculos.mobileXUser(Username, sesion.FirstOrDefault().CompanyID);//Se obtiiene los vehiculos
            return Json(mobiles);
        }

        public async Task<JsonResult> obtenerObtenerMobileAsigandosXUser(string username)//Action para obtener los vehiculos con una bandera para los asignados 
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion
            var mobiles = await iadUsuariosVehiculos.obtenerObtenerMobileAsigandosXUser(username, sesion.FirstOrDefault().CompanyID);//Se obtienen los vehiculos
            return Json(mobiles);

        }

        public async Task<JsonResult> ActulizarMobileXUser(string mobilesid, string Username)//Action para actualizar los vehiculos asignados
        {
            //var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadUsuariosVehiculos.ActulizarMobileXUser(mobilesid, Username);//Se actualizan los vehiculos por usuario
            return Json(resultado);
        }
    }
}
