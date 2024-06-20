using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adAutorizarValesController : Controller
    {
        private readonly IadAutorizarVales iadAutorizarVales;
        public adAutorizarValesController(IadAutorizarVales _IadAutorizarVales)
        {
            iadAutorizarVales = _IadAutorizarVales;
        }
        public IActionResult adAutorizarVales()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerValesGenerados(string FechaIni, string FechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await iadAutorizarVales.ObtenerValesGenerados(FechaIni, FechaFin, user.FirstOrDefault().CompanyID);
            return Json(vales);

        }
        public async Task<JsonResult> ObtenerCentrosCosto()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var CentrosCosto = await iadAutorizarVales.ObtenerCentrosCosto(user.FirstOrDefault().CompanyID);
            return Json(CentrosCosto);
        } 
        public async Task<JsonResult> ObtenerProyectos()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var proyectos = await iadAutorizarVales.ObtenerProyectos(user.FirstOrDefault().CompanyID);
            return Json(proyectos);
        }

        public async Task<JsonResult> ObtenerValeAutorizar(int ValeID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vale = await iadAutorizarVales.ObtenerValeAutorizar(ValeID, user.FirstOrDefault().CompanyID);
            return Json(vale);
        }
    }
}
