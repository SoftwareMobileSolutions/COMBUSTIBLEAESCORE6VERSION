using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;


namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adAdministracionPerfilesController : Controller
    {
        private readonly IadAdministracionPerfiles iadAdministracionPerfiles;

        public adAdministracionPerfilesController(IadAdministracionPerfiles _IadAdministracionPerfiles)
        {
            iadAdministracionPerfiles = _IadAdministracionPerfiles;
        }

        public IActionResult adAdministracionPerfiles()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerPerfiles()//Action para obtener los pefiles
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var perfiles = await iadAdministracionPerfiles.ObtenerPefiles(user.FirstOrDefault().CompanyID);
            return Json(perfiles);
        }

        public async Task<JsonResult> ObtenerModulos(int PefilID)//Action para obtener los modulos
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var modulos = await iadAdministracionPerfiles.ObtenerModulos(PefilID, user.FirstOrDefault().CompanyID);
            return Json(modulos);
        }

        [HttpPost]
        public async Task<JsonResult> CrearPerfil(string NombrePefil)//Action para crear nuevo perfil
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAdministracionPerfiles.CrearPerfil(NombrePefil, user.FirstOrDefault().CompanyID);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarPerfilXModulos(int PerfilID, string NombrePerfil_Nuevo, string ModulosID)//Action para actulizar los perfilles por modulo
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadAdministracionPerfiles.ActualizarPerfilXModulos(user.FirstOrDefault().CompanyID, PerfilID, NombrePerfil_Nuevo, ModulosID);
            return Json(resultado);
        }
    }
}
