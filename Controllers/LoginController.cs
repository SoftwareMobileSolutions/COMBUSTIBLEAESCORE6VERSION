using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class LoginController : Controller
    {
        //
        private readonly ILogin iLogin;

        public LoginController(ILogin _iLogin) { 
            iLogin = _iLogin;
        }
        public async Task<IActionResult> Login()
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            if (sesion != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return PartialView();
            }
        }

        [HttpGet]
        public async Task<JsonResult> iniciarSesion(string username, string password)
        {            
            return await Task.Run(async () => {
                var data = await iLogin.login(username, password);
                //Se verifica si el SP retorna data
                if (data.Count() > 0)
                {
                    //Si regresa data el SP, se verifica si 
                    if (data.FirstOrDefault().FechaActivacion == null)
                    {
                        //Si no se encuentra activo el usuario se le notifica al intentar iniciar sesión 
                        return Json(new { mensaje = "Su usuario aún no se ha activado, por favor ponerse en contacto al siguiente número: 7737 8432", estado = 0 });
                    }
                    else if (data.First().Estado.Equals(1))
                    {
                        //Si la bandera es igual a 1 el usuario entra y se inserta la Data en la sesion
                        _Sesion.Set(HttpContext.Session, "usuario", data);

                        //Se obtienen los menus
                        await setMenu(username, data.FirstOrDefault().CompanyID);
                        //Se inserta los menos en la sesion 
                        return Json(new { mensaje = "Usuario ingresa", estado = 1 });
                    }
                    else
                    {
                       return Json(new { mensaje = "Usuario o contraseña incorrecta", estado = 0 });
                    }

                }
                else
                {
                    //Si no regresa data el SP no existe el usuario o contraseña 
                    return Json(new { mensaje = "Usuario o contraseña incorrecta", estado = 2 });
                }

            });
        }

        protected async Task setMenu(string username,  int CompanyID) //Modulo para obtener los modulos x perfil y la insersion de estos en la sesion
        {
            var modulos = await iLogin.getModulos(username, CompanyID);
            _Sesion.Set(HttpContext.Session, "menu", modulos);
        }

    }
}
