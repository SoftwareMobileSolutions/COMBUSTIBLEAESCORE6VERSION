using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adCerrarValesController : Controller
    {
        private readonly IadCerrarVales iadCerrarVales;

        public adCerrarValesController(IadCerrarVales _iadCerrarVales)
        {
            iadCerrarVales = _iadCerrarVales;
        }

        public IActionResult adCerrarVales()
        {
            return PartialView();
        }

        public async Task<JsonResult> getInformation(string placa)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var data = await iadCerrarVales.getInformationVale(user.First().CompanyID, placa);

            if (data.First().caso == 4)
            {
                _Sesion.Set(HttpContext.Session, "PlacaCerrarVale", placa);
                return Json(data);
            }
            else {
                _Sesion.Set(HttpContext.Session, "PlacaCerrarVale", null);
                return Json(data);
            }
        }

        public async Task<JsonResult> cerrarVale(double CantidadGalones, double TotalDolares, int Odometro) {

            var placa = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");

            var data = await iadCerrarVales.CerrarVale(placa, user.First().UsuarioID, user.First().CompanyID, CantidadGalones, TotalDolares, Odometro);

            _Sesion.Set(HttpContext.Session, "PlacaCerrarVale", null);

            //var prueba = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");
            return Json(data);
        }

        public JsonResult validarPlaca()
        {
            var placa = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");
            return Json(placa);

        }

        public async Task<JsonResult> getValesCerrados(string FechaIncio, string fechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Vales = await iadCerrarVales.getValesCerrados(user.First().UsuarioID, user.First().CompanyID,FechaIncio,fechaFin);
            return Json(Vales);
        }

        public async Task<JsonResult> getValesCerradosXPlaca(string Placa, string FechaIncio, string fechaFin) {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Vales = await iadCerrarVales.getValesCerradosXPlaca(user.First().UsuarioID, user.First().CompanyID, Placa, FechaIncio, fechaFin);
            return Json(Vales);
        }
    }
}
