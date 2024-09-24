using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
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

        public async Task<JsonResult> ObtenerUsuarios()//Action para obtener usuarios por compañia 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Obtiene los datos del usuario de la sesion
            var usuarios = await iadAgregarUsuario.ObtenerUsuarios(user.FirstOrDefault().CompanyID);//Se obtienen los usuarios

            return Json(usuarios);
        }

        public async Task<JsonResult> ObtenerPerfilUsuarios()//Action para obtener perfiles por compañia
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Obtiene los datos del usuario de la sesion
            var perfiles = await iadAgregarUsuario.ObtenerPerfilUsuarios(user.FirstOrDefault().CompanyID);//Se obtienen los perfiles por compañia
            return Json(perfiles);
        }

        public async Task<JsonResult> ObtenerGasolineras()//Action para obtener las gasolineras
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Obtiene los datos del usuario de la sesion
            var gasolineras = await iadAgregarUsuario.ObtenerGasolineras(user.FirstOrDefault().CompanyID);// Se obtienen las gasolineras de la compañía
            return Json(gasolineras);
        }

        [HttpPost]//Action para crear usuarios
        public async Task<JsonResult> CrearUsuarios(string Nombre, string Apellido, string Username, string Clave, string Correo, int PerfilID, int GasolineraID, string Telefono) {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Obtiene los datos del usuario de la sesion

            /*Se intenta crear el usuario y se obtiene el resultado*/
            var resultado = await iadAgregarUsuario.CrearUsuarios(Nombre, Apellido, Username, Clave, Correo, PerfilID, GasolineraID, user.FirstOrDefault().CompanyID, Telefono);
            return Json(resultado);
        }
        [HttpPost]//Action para actualizar usuarios
        public async Task<JsonResult> ActualizarUsuario(string username, string contrasena, string nombre, string apellido, string correo, string telefono)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Obtiene los datos del usuario de la sesion
            /* se intenta actualizar el usuario y se obtiene el resultado*/
            var resultado = await iadAgregarUsuario.ActualizarUsuario(user.FirstOrDefault().CompanyID, username, contrasena, nombre, apellido, correo, telefono);
            return Json(resultado);
        }

        [HttpPost]//Action para cambiar el estado del usuario
        public async Task<JsonResult> CambiarEstadoUsuario(int UsuarioID, int Estado)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Obtiene los datos del usuario de la sesion
            var resultado = await iadAgregarUsuario.CambiarEstadoUsuario(user.FirstOrDefault().CompanyID, UsuarioID, Estado);//Se intenta cambiar el estado y se obtiene el resultado
            return Json(resultado);
        }

        
        /*private void GuardarUsuario(string Username, string contrasena, int PerfilID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");

            if (user.FirstOrDefault().Tutorial == false) { 
                if(PerfilID == 2)
                {
                    _Sesion.Set(HttpContext.Session, "UserGenTuto", Username);
                    _Sesion.Set(HttpContext.Session, "PassGenTuto", contrasena);
                }
                else if (PerfilID == 3){
                    _Sesion.Set(HttpContext.Session, "UserAutTuto", Username);
                    _Sesion.Set(HttpContext.Session, "PassAutTuto", contrasena);
                }
                else if (PerfilID == 4)
                {
                    _Sesion.Set(HttpContext.Session, "UserAutTuto", Username);
                    _Sesion.Set(HttpContext.Session, "PassAutTuto", contrasena);
                }
            }
        }*/
    }
}
