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

        public async Task<JsonResult> ObtenerGasolineras()//Action para obtener las gasolineras 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");// Se obtiene la data del usuario de la sesion
            var Gasonlineras = await iadGasolinera.ObtenerGasolineras(user.FirstOrDefault().CompanyID);//Se obtienen las gasolineras
            return Json(Gasonlineras);
        }

        [HttpPost]
        /*Action para registrar gasolineras*/
        public async Task<JsonResult> CrearGasolinera(string DescriEstacionServicio, float Latitud, float Longitud, string CodigoGasolinera, string CiudadGasolinera, string GerenteGasolinera, string TelefonoGasolinera, string NITGasolinera, string DireccionGasolinera, string EmailGasolinera)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");// Se obtiene la data del usuario de la sesion
            /*Se intenta registrar la gasolinera y se obtiene el resultado*/
            var mensaje = await iadGasolinera.CrearGasolinera(user.FirstOrDefault().CompanyID, DescriEstacionServicio, Latitud, Longitud, CodigoGasolinera, CiudadGasolinera, GerenteGasolinera, TelefonoGasolinera, NITGasolinera, DireccionGasolinera, EmailGasolinera);
            return Json(mensaje);
        }
    }
}
