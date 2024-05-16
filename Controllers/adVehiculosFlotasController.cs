using Microsoft.AspNetCore.Mvc;

using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adVehiculosFlotasController : Controller
    {
        private readonly IadVehiculosFlotas iadVehiculosFlotas;
        public adVehiculosFlotasController(IadVehiculosFlotas _IadVehiculosFlotas) {
            iadVehiculosFlotas = _IadVehiculosFlotas;
        }
        public IActionResult adVehiculosFlotas()
        {
            return PartialView();
        }

        public async Task<JsonResult> agregarVehiculo(string PlacaNew, string NombreNew, string Marca, string Modelo,int FlotaID, float KmXGalon, float CapacidadTanque, int TipoCombustibleID, string VINNew) {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vehiculo = await iadVehiculosFlotas.agregarVehiculo(usuario.FirstOrDefault().CompanyID, PlacaNew,  NombreNew,  Marca,  Modelo, FlotaID,  KmXGalon,  CapacidadTanque, TipoCombustibleID,  VINNew);
            return Json(vehiculo);
        }

        public async Task<JsonResult> obtenerFlotas() {        
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var flotas = await iadVehiculosFlotas.obtenerFlotas(sesion.FirstOrDefault().CompanyID);
            return Json(flotas);
        }

        public async Task<JsonResult> agregarFlota(string SubfleetName) {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var resultado = await iadVehiculosFlotas.agregarFlota(sesion.FirstOrDefault().CompanyID, SubfleetName);
            return Json(resultado);
        }
        public async Task<JsonResult> rpVehiculo(string FechaIni, string FechaFin)
        {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var vehiculos = await iadVehiculosFlotas.rpVehiculo(usuario.FirstOrDefault().CompanyID, FechaIni, FechaFin);
            return Json(vehiculos);
        }
        public async Task<JsonResult> obtenerTipoCombustible()
        {
            var TipoCombustible = await iadVehiculosFlotas.obtenerTipoCombustible();
            return Json(TipoCombustible);
        }
    }
}
