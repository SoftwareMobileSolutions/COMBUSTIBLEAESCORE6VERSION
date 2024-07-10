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
    public class adLiquidarValesController : Controller
    {
        private readonly IadLiquidarVales iadCerrarVales;

        public adLiquidarValesController(IadLiquidarVales _iadCerrarVales)
        {
            iadCerrarVales = _iadCerrarVales;
        }

        public IActionResult adLiquidarVales()
        {
            return PartialView();
        }

        public async Task<JsonResult> getInformation(string placa)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var data = await iadCerrarVales.getInformationVale(user.First().CompanyID, placa);

            if (data.First().caso == 4)
            {
                _Sesion.Set(HttpContext.Session, "PlacaLiquidarVale", placa);
                return Json(data);
            }
            else {
                _Sesion.Set(HttpContext.Session, "PlacaLiquidarVale", null);
                return Json(data);
            }
        }
        [HttpPost]
        public async Task<JsonResult> LiquidarVale(double CantidadGalones, double TotalDolares, int Odometro) {

            var placa = _Sesion.Get<string>(HttpContext.Session, "PlacaLiquidarVale");
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");

            var data = await iadCerrarVales.LiquidarVale(placa, user.First().UsuarioID, user.First().CompanyID, CantidadGalones, TotalDolares, Odometro);

            _Sesion.Set(HttpContext.Session, "PlacaLiquidarVale", null);

            //var prueba = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");
            return Json(data);
        }

        public JsonResult validarPlaca()
        {
            var placa = _Sesion.Get<string>(HttpContext.Session, "PlacaLiquidarVale");
            return Json(placa);

        }

        public async Task<JsonResult> ObtenerValesLiquidados(string FechaIncio, string fechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Vales = await iadCerrarVales.ObtenerValesLiquidados(user.First().UsuarioID, user.First().CompanyID,FechaIncio,fechaFin);
            return Json(Vales);
        }

        public async Task<JsonResult> ObtenerValesLiquidadosXPlaca(string Placa, string FechaIncio, string fechaFin) {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Vales = await iadCerrarVales.ObtenerValesLiquidadosXPlaca(user.First().UsuarioID, user.First().CompanyID, Placa, FechaIncio, fechaFin);
            return Json(Vales);
        }
    }
}
