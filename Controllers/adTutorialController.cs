using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adTutorialController : Controller
    {
        private readonly IadTutorial iadTutorial;

        public adTutorialController(IadTutorial _IadTutorial)
        {
            iadTutorial = _IadTutorial;
        }
        public async Task<IActionResult> adTutorial()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            if (user == null)
            {

                return await Task.Run(() =>
                {
                    return RedirectToAction("Login", "Login");
                });

            }
            else
            {
                var TutorialEstado = user.FirstOrDefault().Tutorial;
                if (TutorialEstado == false)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }

        public async Task<JsonResult> ValidarPaso(int Paso)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mensaje = await iadTutorial.ValidarPaso(user.FirstOrDefault().CompanyID, Paso);
            return Json(mensaje);
        }

        public async Task<JsonResult> TerminarTutorial()
        {

            return await Task.Run(() =>
            {
                var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");

                usuario.FirstOrDefault().Tutorial = true;

                _Sesion.Set(HttpContext.Session, "usuario", null);
                _Sesion.Set(HttpContext.Session, "usuario", usuario);
                return Json(new { bandera = true});
            });
        }
    }
}
