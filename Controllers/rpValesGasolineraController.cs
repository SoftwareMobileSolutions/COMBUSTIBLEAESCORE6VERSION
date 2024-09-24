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
    public class rpValesGasolineraController : Controller
    {
        private readonly IrpValesGasolinera irpValesGasolinera;

        public rpValesGasolineraController(IrpValesGasolinera _IrpValesGasolinera)
        {
            irpValesGasolinera = _IrpValesGasolinera;
        }
        public IActionResult rpValesGasolinera()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerValesXGasolinera(string FechaIni, string FechaFin)//Action para obtener la data
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vales = await irpValesGasolinera.ObtenerValesXGasolinera(user.FirstOrDefault().CompanyID, FechaIni, FechaFin);
            return Json(vales); 
        }
    }
}
