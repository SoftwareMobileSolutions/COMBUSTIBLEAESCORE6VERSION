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
    public class rpValePorSubFlotaController : Controller
    {
        private readonly IrpValePorSubFlota  irpValePorSubFlota;

        public rpValePorSubFlotaController(IrpValePorSubFlota _IrpValePorSubFlota)
        {
            irpValePorSubFlota = _IrpValePorSubFlota;
        }

        public IActionResult rpValePorSubFlota()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerValeXSubFlota(string FechaIni, string FechaFin)//Action para obtener vales por flota
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await irpValePorSubFlota.ObtenerValeXSubFlota(user.FirstOrDefault().CompanyID, FechaIni, FechaFin);
            return Json(vales);
        }
        /*public async Task<JsonResult> ObtenerTopValesPorVehiculo(int NumTop,int OrderByID, string FechaIni, string FechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var TOP = await irpTopValePorVehiculo.ObtenerTopValesPorVehiculo(NumTop, OrderByID, user.FirstOrDefault().CompanyID, FechaIni, FechaFin);
            return Json(TOP); 
        }

        public async Task<JsonResult> ObtenerValesUsadosXPlaca(string Placa, string FechaIncio, string FechaFin)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await irpTopValePorVehiculo.ObtenerValesUsadosXPlaca(user.FirstOrDefault().CompanyID, Placa, FechaIncio, FechaFin);
            return Json(vales);
        }*/
    }

}
