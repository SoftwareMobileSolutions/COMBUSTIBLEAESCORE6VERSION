using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;


namespace COMBUSTIBLEAESCORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment iWebHostEnvironment)
        {
            _logger = logger;
            _iWebHostEnvironment = iWebHostEnvironment;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!string.IsNullOrEmpty(context.HttpContext.Request.Query["culture"]))
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(context.HttpContext.Request.Query["culture"]);
            }
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            //var menu = _Sesion.Get<IEnumerable<modulosModel>>(HttpContext.Session, "menu");
            if (user == null)
            {

                return await Task.Run(() => {
                    return RedirectToAction("Login", "Login");
                });

            }
            else
            {
                ViewData["user"] = user.FirstOrDefault().Username;
                ViewData["CompanyName"] = user.FirstOrDefault().CompanyName;
                ViewData["modulos"] = sortMenu();
                ViewData["CompamyLogo"] = logoCompany(user.FirstOrDefault().CompanyID);
                ViewData["Name"] = user.FirstOrDefault().Nombre + " " + user.FirstOrDefault().Apellido;
                ViewData["DataUserFull"] = user;
                //ViewBag.login = user.FirstOrDefault().login;
                return View();
            }

        }

        protected JsonResult sortMenu()
        {
            var menu = _Sesion.Get<IEnumerable<ModulosModel>>(HttpContext.Session, "menu");

            var grandParent = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                url = t.UrlPage,
                Nivel = t.Nivel
            }).Where(t => t.Nivel == 0);

            var parent = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                parent = t.Parent,
                url = t.UrlPage,
                Nivel = t.Nivel
            }).Where(t => t.Nivel == 1);

            var children = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                grandParent = t.GrandParent,
                parent = t.Parent,
                url = t.UrlPage,
                Nivel = t.Nivel
            }).Where(t => t.Nivel == 2);

            var arbolMenu = grandParent.Select(gp => new
            {
                name = gp.Name,
                codigo = gp.codigo,
                url = gp.url,
                nivel = gp.Nivel,
                HasChildren = parent.Any(p => p.parent == gp.codigo),
                Children = parent.Select(p => new
                {
                    Name = p.Name,
                    codigo = p.codigo,
                    parent = p.parent,
                    url = p.url,
                    Nivel = p.Nivel,
                    HasChildren = children.Any(ch => ch.grandParent == p.parent),
                    children = children.Select(c => new {
                        Name = c.Name,
                        codigo = c.codigo,
                        grandParent = c.grandParent,
                        parent = c.parent,
                        url = c.url,
                        Nivel = c.Nivel
                    }).Where(ch => ch.grandParent == p.parent)

                }).Where(p => p.parent == gp.codigo)
            });

            return Json(arbolMenu);
        }


        protected String logoCompany(int CompanyID)
        {
            var logoSRC = "/images/default-logo.png";
            if (System.IO.File.Exists(Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.jpg")))
            {
                // return "";
                //bg = 1;
                logoSRC = "/images/CompanyLogo/" + "Company" + CompanyID + "_logo.jpg";
            }
            if (System.IO.File.Exists(Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.png")))
            {
                //bg = 2;
                //return "";
                logoSRC = "/images/CompanyLogo/" + "Company" + CompanyID + "_logo.png";
            }

            return logoSRC;
        }

        [HttpGet]
        public async Task<JsonResult> LogOut()
        {
            var requiredKeys = HttpContext.Session.Keys;

            foreach (var cookieKey in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Delete(cookieKey);
            }

            foreach (var key in requiredKeys)
            {
                HttpContext.Session.SetString(key, JsonConvert.SerializeObject(null));
            }
            return Json(requiredKeys);
        }

        
        public async Task<JsonResult> GuardarModulo(string modulo)
        {
            return await Task.Run(() => {
                _Sesion.Set(HttpContext.Session, "modulo", modulo);
                return Json(new { mensaje = "Se guardo el modulo" });
            });
        }

        public async Task<string> ObtenerModulo()
        {
            return await Task.Run(() => {
                var modulo = _Sesion.Get<string>(HttpContext.Session, "modulo");
                return (modulo);
            });
        }
    }
}
