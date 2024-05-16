using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adProyectosController : Controller
    {
        private readonly IadProyecto iProyecto;
        public adProyectosController(IadProyecto _IProyecto)
        {
            iProyecto = _IProyecto;
        }
        public IActionResult adProyectos()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> InsertarPEPS(string PEPS_NombreProyecto, string Responsable_Estado) {

            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iProyecto.InsertarPEPS(PEPS_NombreProyecto, Responsable_Estado, user.FirstOrDefault().CompanyID, user.FirstOrDefault().UsuarioID);
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerPEPS()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var PEPS = await iProyecto.ObtenerPEPS(user.FirstOrDefault().CompanyID);
            return Json(PEPS);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarProyecto(int ProyectoID, string Responsable, int Estado, int Bandera)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iProyecto.ActualizarProyecto(user.FirstOrDefault().CompanyID, ProyectoID, Responsable, Estado, Bandera);
            return Json(resultado);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarProyecto(int ProyectoID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iProyecto.EliminarProyecto(ProyectoID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }
    }
}
