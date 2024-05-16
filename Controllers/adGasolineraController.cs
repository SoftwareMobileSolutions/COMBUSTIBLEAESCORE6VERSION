using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adGasolineraController : Controller
    {
        private readonly IadGasolinera iadGasolinera;

        public adGasolineraController(IadGasolinera _IadGasolinera)
        {
            iadGasolinera = _IadGasolinera;
        }
        public IActionResult adGasolinera()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerGasolineras()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Gasonlineras = await iadGasolinera.ObtenerGasolineras(user.FirstOrDefault().CompanyID);
            return Json(Gasonlineras);
        }

        [HttpPost]

        public async Task<JsonResult> CrearGasolinera(string DescriEstacionServicio, float Latitud, float Longitud)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var mensaje = await iadGasolinera.CrearGasolinera(user.FirstOrDefault().CompanyID, DescriEstacionServicio, Latitud, Longitud);
            return Json(mensaje);
        }
    }
}
