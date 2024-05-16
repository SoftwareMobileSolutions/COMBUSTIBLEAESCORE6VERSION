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
    public class adAgregarUsuarioController : Controller
    {
        private readonly IadAgregarUsuario iadAgregarUsuario;

        public adAgregarUsuarioController(IadAgregarUsuario _IadAgregarUsuario)
        {
            iadAgregarUsuario = _IadAgregarUsuario;
        }

        public IActionResult adAgregarUsuario()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerUsuarios()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var usuarios = await iadAgregarUsuario.ObtenerUsuarios(user.FirstOrDefault().CompanyID);

            return Json(usuarios);
        }

        public async Task<JsonResult> ObtenerPerfilUsuarios()
        {
            var perfiles = await iadAgregarUsuario.ObtenerPerfilUsuarios();
            return Json(perfiles);
        }

        public async Task<JsonResult> ObtenerGasolineras()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var gasolineras = await iadAgregarUsuario.ObtenerGasolineras(user.FirstOrDefault().CompanyID);
            return Json(gasolineras);
        }

        [HttpPost]
        public async Task<JsonResult> CrearUsuarios(string Nombre, string Apellido, string Username, string Clave, string Correo, int PerfilID,int GasolineraID, string Telefono) {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAgregarUsuario.CrearUsuarios(Nombre, Apellido, Username, Clave, Correo, PerfilID, GasolineraID, user.FirstOrDefault().CompanyID, Telefono);
            return Json(resultado);
        }
        [HttpPost] 
        public async Task<JsonResult> ActualizarUsuario(string username, string contrasena, string nombre, string apellido, string correo, string telefono)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAgregarUsuario.ActualizarUsuario(user.FirstOrDefault().CompanyID, username, contrasena, nombre, apellido, correo, telefono);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> CambiarEstadoUsuario(int UsuarioID, int Estado)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAgregarUsuario.CambiarEstadoUsuario(user.FirstOrDefault().CompanyID, UsuarioID, Estado);
            return Json(resultado);
        }
    }
}
