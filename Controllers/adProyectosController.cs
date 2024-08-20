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
        [HttpPost]// Action para insertar proyectos 
        public async Task<JsonResult> InsertarProyectos(string CodigoProyecto_NombreProyecto, string Responsable_Estado) {

            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtienen la data del usuario de la sesion
            /*Se intenta registrar los proyectos y se recibe el resultado*/
            var resultado = await iProyecto.InsertarProyectos(CodigoProyecto_NombreProyecto, Responsable_Estado, user.FirstOrDefault().CompanyID, user.FirstOrDefault().UsuarioID);
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerProyectos()// Action para obtener proyectos 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtienen la data del usuario de la sesion
            var PEPS = await iProyecto.ObtenerProyectos(user.FirstOrDefault().CompanyID); //Se obtienen los proyectos de la compañia
            return Json(PEPS);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarProyecto(int ProyectoID, string Responsable, int Estado, int Bandera)// Action para actualizar proyectos 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtienen la data del usuario de la sesion
            /*Se intenta actulizar el proyecto y se recibe el resultado*/
            var resultado = await iProyecto.ActualizarProyecto(user.FirstOrDefault().CompanyID, ProyectoID, Responsable, Estado, Bandera); 
            return Json(resultado);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarProyecto(int ProyectoID)//Action para eliminar proyctos de manera logica 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtienen la data del usuario de la sesion
            /*Se intenta eliminar le proyecto y se obitne el resultado*/
            var resultado = await iProyecto.EliminarProyecto(ProyectoID, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }
    }
}
