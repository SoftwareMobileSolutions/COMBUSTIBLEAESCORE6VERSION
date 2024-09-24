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
    public class adCerrarValeController : Controller
    {
        private readonly IadCerrarVale iadCerrarVales;

        public adCerrarValeController(IadCerrarVale _iadCerrarVales)
        {
            iadCerrarVales = _iadCerrarVales;
        }

        public IActionResult adCerrarVale()
        {
            return PartialView();
        }

        public async Task<JsonResult> getInformation(string placa)//Action para obtener la información de un vale por cerrar
        {
            /*
             Nota: 
             Caso 2 = Hay un vale generado pero no autorizado para esa placa
             Caso 3 = La placa no tiene vales 
             Caso 4 = Se encontro un vale para cerrar a esa placa
             */
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion
            var data = await iadCerrarVales.getInformationVale(user.First().CompanyID, placa);//Se obtiene la información del vale

            if (data.First().caso == 4)
            {
                _Sesion.Set(HttpContext.Session, "PlacaCerrarVale", placa);//Si se encontro una vale, se guarda la placa para que no se pueda cerrar por error el vale de otra placa
                return Json(data);
            }
            else {
                _Sesion.Set(HttpContext.Session, "PlacaCerrarVale", null);//Si no hay vale se setea null 
                return Json(data);
            }
        }
        [HttpPost]
        public async Task<JsonResult> CerrarVale(double CantidadGalones, double TotalDolares, int Odometro) {//Action para cerra vales 

            var placa = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");//Se obtienen la placa del vale que se obtuvo la información
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data de usuario de la sesion

            /*Se intenta cerrar el vale*/
            var data = await iadCerrarVales.CerrarVale(placa, user.First().UsuarioID, user.First().CompanyID, CantidadGalones, TotalDolares, Odometro);

            _Sesion.Set(HttpContext.Session, "PlacaCerrarVale", null);//Se setea null en la sesion donde se guardo el vale 

            //var prueba = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");
            return Json(data);//Se retorna el resultado
        }

        public JsonResult validarPlaca()//Action para obtener la placa guardada en la sesion
        {
            var placa = _Sesion.Get<string>(HttpContext.Session, "PlacaCerrarVale");//Se obtiene la placa de la sesion
            return Json(placa);//Se retorna el resultado

        }

        public async Task<JsonResult> ObtenerValesCerrados(string FechaIncio, string fechaFin)//Se obtiene los vales liquidados
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion
            var Vales = await iadCerrarVales.ObtenerValesCerrados(user.First().UsuarioID, user.First().CompanyID,FechaIncio,fechaFin);//Se obtiene los vales
            return Json(Vales);
        }

        public async Task<JsonResult> ObtenerValesCerradosXPlaca(string Placa, string FechaIncio, string fechaFin) {//Action para obtener los vales cerrados por placa
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion
            var Vales = await iadCerrarVales.ObtenerValesCerradosXPlaca(user.First().UsuarioID, user.First().CompanyID, Placa, FechaIncio, fechaFin);//Se obtiene la data de los vales por placa
            return Json(Vales);
        }
    }
}
