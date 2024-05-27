using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ImageMagick;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adConfiguracionCompanyController : Controller
    {
        private readonly IadConfiguracionCompany iadConfiguracionCompany;
        private readonly IWebHostEnvironment iWebHostEnvironment;

        public adConfiguracionCompanyController(IadConfiguracionCompany _IadConfiguracionCompany, IWebHostEnvironment _IWebHostEnvironment)
        {
            iadConfiguracionCompany = _IadConfiguracionCompany;
            iWebHostEnvironment = _IWebHostEnvironment;
        }
        public IActionResult adConfiguracionCompany()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerDataDrops()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var Company = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID);

            var departamentos = await iadConfiguracionCompany.ObtenerDepartamentos();
            var municipios = await iadConfiguracionCompany.ObtenerMunicipios(Company.FirstOrDefault().DepartamentoID);

            
            return Json(new { departamentos, Company.FirstOrDefault().DepartamentoID, municipios, Company.FirstOrDefault().MunicipioID });
        }

        public async Task<JsonResult> ObtenerMunicipios(int DepartamentoID)
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var municipios = await iadConfiguracionCompany.ObtenerMunicipios(DepartamentoID);

            var Company = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID);
            return Json(new { municipios, Company.FirstOrDefault().MunicipioID });
        }
        public async Task<JsonResult> ObtenerDataCompany()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var DataCompany = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID);
            var Logo = logoCompany(user.FirstOrDefault().CompanyID);

            return Json(new { Logo, DataCompany });
        }

        protected String logoCompany(int CompanyID)
        {
            var logoSRC = "images/CompanyLogo/default_logo.png";
            if (System.IO.File.Exists(Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.jpg")))
            {
                logoSRC = "images/CompanyLogo/" + "Company" + CompanyID + "_logo.jpg";
            }
            if (System.IO.File.Exists(Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.png")))
            {
                logoSRC = "images/CompanyLogo/" + "Company" + CompanyID + "_logo.png";
            }

            return logoSRC;
        }

        [HttpPost]
        public async Task<JsonResult> CambiarInformacion(string NombreCompany, string Direccion, string SitioWeb, string Correo, string RazonSocial, int? DepartamentoID, int? MunicipioID, string Descripcion, string TelMovil, string Telfijo, IFormFile img)
        {
             var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            if (img != null && img.Length > 0)
            {
                if(ActualizarBackGround(img) == true)
                {
                    var mensaje = await iadConfiguracionCompany.ActualizarInformacion(user.FirstOrDefault().CompanyID, NombreCompany, Direccion, SitioWeb, Correo, RazonSocial, DepartamentoID, MunicipioID, Descripcion, TelMovil, Telfijo);
                    return Json(mensaje);
                }
                else
                {
                    return Json(new { resultado  = "No se pudo actaulizar la informacion de la empresa"});
                }

            }
            else
            {
                var mensaje = await iadConfiguracionCompany.ActualizarInformacion(user.FirstOrDefault().CompanyID, NombreCompany, Direccion, SitioWeb, Correo, RazonSocial, DepartamentoID, MunicipioID, Descripcion, TelMovil, Telfijo);
                return Json(mensaje);
            }
        }

        public async Task<JsonResult> GuardarDataCompany()
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
            var dataCompany = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID);
            //return Json();
            return Json(new
            {
                CompanyName = dataCompany.FirstOrDefault().NombreCompany, //user.FirstOrDefault().CompanyName,
                CompamyLogo = logoCompany(user.FirstOrDefault().CompanyID),
                Cotancto = (dataCompany.FirstOrDefault().TelMovil is null? "": dataCompany.FirstOrDefault().TelMovil),//(user.FirstOrDefault().TelMovil is null ? "" : user.FirstOrDefault().TelMovil),
                Direccion = dataCompany.FirstOrDefault().Direccion
            });
            /*return await Task.Run(() => {
                var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
                return Json(new { CompanyName = user.FirstOrDefault().CompanyName, CompamyLogo = logoCompany(user.FirstOrDefault().CompanyID), Cotancto = user.FirstOrDefault().TelMovil, Direccion = user.FirstOrDefault().Direccion });
            });*/
        }

        protected bool ActualizarBackGround(IFormFile img)
        {
            try{
                var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");
                int CompanyID = user.FirstOrDefault().CompanyID;
                string fileName = "Company" + CompanyID + "_logo" + Path.GetExtension(img.FileName);
                string filePath = Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", fileName);

                string[] existingFiles = Directory.GetFiles(Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo"), "Company" + CompanyID + "_logo.*");
                foreach (string existingFile in existingFiles)
                {
                    System.IO.File.Delete(existingFile);
                }
                using (var image = new MagickImage(img.OpenReadStream()))
                {
                    image.Quality = 70;
                    image.Resize(800, 600);
                    image.Strip();
                    image.Write(filePath);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
