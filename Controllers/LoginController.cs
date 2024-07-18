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
                if (data.Count() > 0)
                {
                    if (data.First().Estado.Equals(1))
                    {
                        _Sesion.Set(HttpContext.Session, "usuario", data);

                        await setMenu(username);

                        return Json(new { mensaje = "Usuario ingresa", estado = 1 });
                    }
                    else
                    {
                        return Json(new { mensaje = "Su usuario aún no se ha activado, por favor ponerse en contacto al siguiente número: 7737 8432", estado = 0 });
                        //return Json(new { mensaje = "Usuario o contraseña incorrecta", estado = 0 });
                    }
                }
                else
                {
                    return Json(new { mensaje = "Usuario o contraseña incorrecta", estado = 2 });
                }

            });
        }

        protected async Task setMenu(string username)
        {
            var modulos = await iLogin.getModulos(username);
            _Sesion.Set(HttpContext.Session, "menu", modulos);
        }

    }
}
