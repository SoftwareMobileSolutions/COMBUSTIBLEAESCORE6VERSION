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

        /*Action para agregar vehiculos*/
        public async Task<JsonResult> agregarVehiculo(string PlacaNew, string NombreNew, string Marca, string Modelo,int FlotaID, float KmXGalon, float CapacidadTanque, int TipoCombustibleID, string VINNew, int CentroCostoID) {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var vehiculo = await iadVehiculosFlotas.agregarVehiculo(usuario.FirstOrDefault().CompanyID, PlacaNew,  NombreNew,  Marca,  Modelo, FlotaID,  KmXGalon,  CapacidadTanque, TipoCombustibleID,  VINNew, CentroCostoID,
                usuario.FirstOrDefault().UsuarioID
                );//se intenta registrar el vehiculo y se obtiene el resultado
            return Json(vehiculo);
        }

        public async Task<JsonResult> obtenerFlotas() {//Action para obtener flotas    
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var flotas = await iadVehiculosFlotas.obtenerFlotas(sesion.FirstOrDefault().CompanyID);//Se obtienen las subflotas por compañia
            return Json(flotas);
        }

        public async Task<JsonResult> ObtenerCentrosCosto()//Action para obtener los centros de costo
        {
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var centrosCosto = await iadVehiculosFlotas.ObtenerCentrosCosto(sesion.FirstOrDefault().CompanyID); //Se obtienen los centros de costo
            return Json(centrosCosto);
        }
        public async Task<JsonResult> agregarFlota(string SubfleetName) {//Action para agregar flota
            var sesion = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var resultado = await iadVehiculosFlotas.agregarFlota(sesion.FirstOrDefault().CompanyID, SubfleetName);//Se intenta crear la subflota y se obtiene el resultado
            return Json(resultado);
        }
        public async Task<JsonResult> rpVehiculo(string FechaIni, string FechaFin)
        {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var vehiculos = await iadVehiculosFlotas.rpVehiculo(usuario.FirstOrDefault().CompanyID, FechaIni, FechaFin);//Se obtienen los vehiculos por compañia 
            return Json(vehiculos);
        }
        public async Task<JsonResult> obtenerTipoCombustible()//Action para obtener los tipos de combustible
        {
            var TipoCombustible = await iadVehiculosFlotas.obtenerTipoCombustible();//Se obtienen los tipos de combustible
            return Json(TipoCombustible);
        }

        [HttpPost]//Action para actualizar los vehiculos
        public async Task<JsonResult> ActualizaMobile(int MobileID, string Placa, string Nombre, int FlotaID, string Marca, string Modelo, float? KmXGalon, float? CapTan, int? CombustibleID, string NumeroVIm)
        {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var mensaje = await iadVehiculosFlotas.ActualizaMobile(MobileID, usuario.FirstOrDefault().CompanyID, Placa,  Nombre, FlotaID,  Marca,  Modelo,  KmXGalon,  CapTan,  CombustibleID, NumeroVIm);
            return Json(mensaje);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarMobile(int MobileID)//Action para eliminar mobile
        {
            var mensaje = await iadVehiculosFlotas.EliminarMobile(MobileID);//Se intenta elminar un vehiculo y se obtiene el resultado 
            return Json(mensaje);
        }

        [HttpPost]
        public async Task<JsonResult> CrearCentroCosto(string Nombre)//Action para crear centros de costo
        {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var mensaje = await iadVehiculosFlotas.CrearCentroCosto(Nombre, usuario.FirstOrDefault().CompanyID);//Se obtienen los centros de costo
            return Json(mensaje);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarSubfleet(int SubfleetID)//Action para eliminar subflotas
        {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario la sesion
            var mensaje = await iadVehiculosFlotas.EliminarSubfleet(SubfleetID, usuario.FirstOrDefault().CompanyID);//Se elimina la subflota
            return Json(mensaje);
        }

        [HttpPost]
        public async Task<JsonResult> ActulizarSubfleet(int SubfleetID, string NombreSubfleetNuevo)//Action para actualizar la subflota
        {
            var usuario = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtine la data del usuaroi de la sesion
            var mensaje = await iadVehiculosFlotas.ActualizarSubfleet(SubfleetID, usuario.FirstOrDefault().CompanyID, NombreSubfleetNuevo);
            return Json(mensaje);
        }

        public async Task<JsonResult> ObtenerMobileXSubfleet(int SubfleetID)//Action para obtener los mobile por subflota
        {
            var mobiles = await iadVehiculosFlotas.ObtenerMobileXSubfleet(SubfleetID);//Se obtienen los vehiculos por subflota
            return Json(mobiles);
        }
    }
}
