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
    public class adRazonesCancelacionController : Controller
    {
        private readonly IadRazonesCancelacion iadRazonesCancelacion;

        public adRazonesCancelacionController(IadRazonesCancelacion _iadRazonesCancelacion)
        {
            iadRazonesCancelacion = _iadRazonesCancelacion;
        }

        public IActionResult adRazonesCancelacion()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> crearRazonCancelacion(string Razon) //Action para crear las razones de cancelación
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");// Se obtiene la data del usuario en la sesion 
            var resultado = await iadRazonesCancelacion.crearRazonCancelacion(Razon, user.FirstOrDefault().CompanyID); // Se obtiene el resultado 
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerRazonCancelacion()//Action para obtener las razones de cancelación 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");// Se obtiene la data del usuario en la sesion
            var razones = await iadRazonesCancelacion.ObtenerRazonCancelacion(user.FirstOrDefault().CompanyID);// Se obtienen las razones
            return Json(razones);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarRazonCancelacion(int RazonValeCanceladoID, string RazonNuevo)//Action para actualizar las razones de cancelacion 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");// Se obtiene la data del usuario en la sesion
            var resultado = await iadRazonesCancelacion.ActualizarRazonCancelacion(RazonValeCanceladoID, user.FirstOrDefault().CompanyID, RazonNuevo);//Se obtinee el resultado 
            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarRazonCancelacion(int RazonValeCanceladoID)//Action pata eliminar las razones de cancelación
        {   /*Nota: No se elimina la razon de manera literal, solamente logica*/
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");// Se obtiene la data del usuario en la sesion
            var resultado = await iadRazonesCancelacion.EliminarRazonCancelacion(RazonValeCanceladoID,user.FirstOrDefault().CompanyID);//Se obtinee el resultado 
            return Json(resultado);
        }
    }
}
