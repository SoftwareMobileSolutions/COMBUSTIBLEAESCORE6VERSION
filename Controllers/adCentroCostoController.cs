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
    public class adCentroCostoController : Controller
    {
        private readonly IadCentroCosto iadCentroCosto;

        public adCentroCostoController(IadCentroCosto _IadCentroCosto)
        {
            iadCentroCosto = _IadCentroCosto; // Se inicializa la interfas en el constructor 
        }
        public IActionResult adCentroCosto()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> crearCentroCosto(string Nombre) { //Action para crear centros de costo

            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesion 
            var resultado = await iadCentroCosto.crearCentroCosto(Nombre, user.FirstOrDefault().CompanyID); // Se obtienen el resultado y la bander es 1 se creo caso contrario ocurrio un error
            return Json(resultado);
        }

        public async Task<JsonResult> ObtenerCentrosCosto() //Action para obtener los centros de costo 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesion 
            var CentrosCosto = await iadCentroCosto.ObtenerCentrosCosto(user.FirstOrDefault().CompanyID); // Se obtienen los centros de costo
            return Json(CentrosCosto);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarCentrosCosto(int CentroCostoID, string NombreNuevo) //Action para actulizar la data del centro de costo
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesion 
            var resultado = await iadCentroCosto.ActualizarCentrosCosto(CentroCostoID, user.FirstOrDefault().CompanyID, NombreNuevo);//Se obtine el resultado, si la bandera es 1 fue exitoso, caso contrario ocurrio error 
            return Json(resultado);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarCentroCosto(int CentroCostoID)//Action para eliminar los centros de costo 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesion 
            var resultado = await iadCentroCosto.EliminarCentroCosto(CentroCostoID, user.FirstOrDefault().CompanyID);//Se obtiene el resultado, si la bandera es 1 fue exitoso, caso contrario ocurrio error 
            return Json(resultado);
            /*Notas: Los centros de costo no se eliminan literalmente, solo de manera logica, el registro queda en las bases de datos*/
        }
    }
}
