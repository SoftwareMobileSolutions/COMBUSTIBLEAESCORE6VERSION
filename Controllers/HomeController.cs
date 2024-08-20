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
            //Se obtiene los datos del usuario de la sesión 
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            //var menu = _Sesion.Get<IEnumerable<modulosModel>>(HttpContext.Session, "menu");
            if (user == null)
            {
                //Si no hya datos en la sesion se retorna al login
                return await Task.Run(() => {
                    return RedirectToAction("Login", "Login");
                });

            }
            else
            {   //Si hay datos en la sesion 

                
                var TutorialEstado = user.FirstOrDefault().Tutorial;
                //Se verifica si ya termino el tutorial al crear la compañia
                if (TutorialEstado == false)
                {   //En caso de no tener concluido el tutorial se redirecciona al tutorial 
                    return RedirectToAction("adTutorial", "adTutorial");
                }else
                {
                    //Si ya termino el tutorial se comparte informacion a la vista :
                    ViewData["user"] = user.FirstOrDefault().Username; // nombre de usuario
                    ViewData["CompanyName"] = user.FirstOrDefault().CompanyName; // Nombre de la compañia
                    ViewData["modulos"] = sortMenu(); // los modulo a los que tiene acceso el usuario 
                    ViewData["CompamyLogo"] = logoCompany(user.FirstOrDefault().CompanyID); // El logo de la compañia  
                    ViewData["Name"] = user.FirstOrDefault().Nombre + " " + user.FirstOrDefault().Apellido; // El nombre y apellido del usuario
                    //ViewData["DataUserFull"] = user; 

                    /*ViewData["Cotancto"] = user.FirstOrDefault().TelMovil;
                    ViewData["Direccion"] = user.FirstOrDefault().Direccion;*/
                    //ViewBag.login = user.FirstOrDefault().login;
                    return View();
                }

            }

        }

        public async Task<JsonResult> GuardarDataCompany()
        {   //Esta action retorna la data de la company para ser guardada desde la vista en el localStorage
            return await Task.Run(() => {
                var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
                return Json(new { CompanyName = user.FirstOrDefault().CompanyName, CompamyLogo = logoCompany(user.FirstOrDefault().CompanyID) , 
                    Cotancto  = (user.FirstOrDefault().TelMovil is null? "": user.FirstOrDefault().TelMovil), Direccion = user.FirstOrDefault().Direccion });
            });
        }

        protected JsonResult sortMenu() //Action para guardar retornar el menú  ordenado 
        {
            //Se obtienen los modulos de la sesión
            var menu = _Sesion.Get<IEnumerable<ModulosModel>>(HttpContext.Session, "menu");

            //Se obtienen los modulos abuelos
            var grandParent = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                url = t.UrlPage,
                Nivel = t.Nivel,
                Icono = t.Icono,
                Descriptcion = t.Descriptcion
            }).Where(t => t.Nivel == 0);

            //Se obtienen los modulos padres
            var parent = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                parent = t.Parent,
                url = t.UrlPage,
                Nivel = t.Nivel,
                Icono = t.Icono,
                Descriptcion = t.Descriptcion
            }).Where(t => t.Nivel == 1);

            //Se obtienen los modulos hijos
            var children = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                grandParent = t.GrandParent,
                parent = t.Parent,
                url = t.UrlPage,
                Nivel = t.Nivel,
                Descriptcion = t.Descriptcion
            }).Where(t => t.Nivel == 2);

            //Se genera el arbol 
            var arbolMenu = grandParent.Select(gp => new
            {
                name = gp.Name,
                codigo = gp.codigo,
                url = gp.url,
                nivel = gp.Nivel,
                Icono = gp.Icono,
                Descriptcion= gp.Descriptcion,
                HasChildren = parent.Any(p => p.parent == gp.codigo), //Se añaden los modulos padres a los abuelos en base al cod y el parent 
                Children = parent.Select(p => new
                {
                    Name = p.Name,
                    codigo = p.codigo,
                    parent = p.parent,
                    url = p.url,
                    Nivel = p.Nivel,
                    Icono = p.Icono,
                    Descriptcion= p.Descriptcion,
                    HasChildren = children.Any(ch => ch.parent == p.codigo),//Se añaden los hijos a los padres en base al cod y el parent 
                    children = children.Select(c => new {
                        Name = c.Name,
                        codigo = c.codigo,
                        grandParent = c.grandParent,
                        parent = c.parent,
                        url = c.url,
                        Nivel = c.Nivel,
                        Descriptcion = c.Descriptcion,
                    }).Where(ch => ch.parent == p.codigo)

                }).Where(p => p.parent == gp.codigo)
            });
            //Nota si no hay un nivel hijos, solo se queda en padre y abuelos
            return Json(arbolMenu);
        }

        
        protected String logoCompany(int CompanyID) // Action para colocar la imagen de la compañia 
        {
            var logoSRC = "images/CompanyLogo/default_logo.png"; //Logo por defecto
            if (System.IO.File.Exists(Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.jpg"))) //Se verifica si exsite un logo en formato jpg
            {
                // return "";
                //bg = 1;
                logoSRC = "images/CompanyLogo/" + "Company" + CompanyID + "_logo.jpg"; //Se retorna logo en JPG
            }
            if (System.IO.File.Exists(Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.png"))) // Se verifica si existe un logo en formato PNG
            {
                //bg = 2;
                //return "";
                logoSRC = "images/CompanyLogo/" + "Company" + CompanyID + "_logo.png"; // Se retorna logo en PNG
            }

            return logoSRC; // Se retorna el logo
        }

        [HttpGet]
        public async Task<JsonResult> LogOut() //Action para cerrar sesión 
        {
            var requiredKeys = HttpContext.Session.Keys; // Se obtienen las Keys

            foreach (var cookieKey in HttpContext.Request.Cookies.Keys) //Se eliminan las cookies 
            {
                HttpContext.Response.Cookies.Delete(cookieKey);
            }

            foreach (var key in requiredKeys) // Se elimina los daros de la sesión
            {
                HttpContext.Session.SetString(key, JsonConvert.SerializeObject(null));
            }
            return Json(requiredKeys);
        }

        
        public async Task<JsonResult> GuardarModulo(string modulo) //Se guarda el _mod se esta viendo en la sesion, así al recargar no cambía 
        {
            return await Task.Run(() => {
                _Sesion.Set(HttpContext.Session, "modulo", modulo); // Se guarda el _modulo en la sesion 
                return Json(new { mensaje = "Se guardo el modulo" });
            });
        }

        public async Task<string> ObtenerModulo()//Action para obtener el _modulo que se esta gurdado en la sesión y mostrarlo en la vista 
        {
            return await Task.Run(() => {
                var modulo = _Sesion.Get<string>(HttpContext.Session, "modulo");  // Se retorna el _modulo que esta en la sesion 
                return (modulo);
            });
        }

        //nota _modulo son las vistas del  menú
    }
}
