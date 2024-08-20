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

        public async Task<JsonResult> ObtenerDataDrops()//Action pata obtener la data de los Dropdownlists
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesión
            var Company = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID);//Se obtiene la data de la compañia 

            var departamentos = await iadConfiguracionCompany.ObtenerDepartamentos(); //Se obtienen los departamentos
            var municipios = await iadConfiguracionCompany.ObtenerMunicipios(Company.FirstOrDefault().DepartamentoID); // Se obtinen los municipios

            
            return Json(new { departamentos, Company.FirstOrDefault().DepartamentoID, municipios, Company.FirstOrDefault().MunicipioID }); 
        }

        public async Task<JsonResult> ObtenerMunicipios(int DepartamentoID) // Action pata obtener los municipios en base a la selecion del dropdown de los departamentos
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesión
            var municipios = await iadConfiguracionCompany.ObtenerMunicipios(DepartamentoID); // Se obtinen los municipios

            var Company = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID); //Se obtiene la data, de la company, si ya tiene un municipio seleccionado se marque anque cambie de departamento
            return Json(new { municipios, Company.FirstOrDefault().MunicipioID });
        }
        public async Task<JsonResult> ObtenerDataCompany() //Action para obtener la data la compañia
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesión
            var DataCompany = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID); //Se obtiene la data de la compañia
            var Logo = logoCompany(user.FirstOrDefault().CompanyID);

            return Json(new { Logo, DataCompany });
        }

        protected String logoCompany(int CompanyID) // Metodo para obtener el logo de la empresa 
        {
            var logoSRC = "images/CompanyLogo/default_logo.png"; // Logo por defecto 
            if (System.IO.File.Exists(Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.jpg"))) // Se verifica si hay logo con extension jpg
            {
                logoSRC = "images/CompanyLogo/" + "Company" + CompanyID + "_logo.jpg"; //Se retorna la raiz del logo con formato jpg
            }
            if (System.IO.File.Exists(Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", "Company" + CompanyID + "_logo.png"))) // Se verifica si hay logo con extension png
            {
                logoSRC = "images/CompanyLogo/" + "Company" + CompanyID + "_logo.png"; //Se retorna la raiz del logo con formato PNG
            }

            return logoSRC;
        }

        [HttpPost]//Action pata actulizar la información de la compañia 
        public async Task<JsonResult> CambiarInformacion(string NombreCompany, string Direccion, string SitioWeb, string Correo, string RazonSocial, int? DepartamentoID, int? MunicipioID, string Descripcion, string TelMovil, string Telfijo, IFormFile img)
        {
             var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesión
            if (img != null && img.Length > 0) //Se verifica si el usuario cambió la imagen 
            {
                if(ActualizarBackGround(img) == true) //Se llama al metodo para guardar la imagen en el server y se verifica si fue exitoso 
                {   //En caso de exito se guarda la informacion de la compañia junto a la imagen
                    var mensaje = await iadConfiguracionCompany.ActualizarInformacion(user.FirstOrDefault().CompanyID, NombreCompany, Direccion, SitioWeb, Correo, RazonSocial, DepartamentoID, MunicipioID, Descripcion, TelMovil, Telfijo);
                    return Json(mensaje);
                }
                else
                {   //En caso de error se retorna  el siguiente mensaje
                    return Json(new { resultado  = "No se pudo actaulizar la informacion de la empresa"});
                }

            }
            else
            {   //En caso quje el usuario no cambió la imagen se guarda la información de la compañia 
                var mensaje = await iadConfiguracionCompany.ActualizarInformacion(user.FirstOrDefault().CompanyID, NombreCompany, Direccion, SitioWeb, Correo, RazonSocial, DepartamentoID, MunicipioID, Descripcion, TelMovil, Telfijo);
                return Json(mensaje);
            }
        }

        public async Task<JsonResult> GuardarDataCompany() //Action para obtener la data de la company y guardar la data desde la sesion 
        {
            var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario");//Se obtiene la data del usuario de la sesión
            var dataCompany = await iadConfiguracionCompany.ObtenerDataCompany(user.FirstOrDefault().CompanyID);//Se obtiene la data de la company
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

        protected bool ActualizarBackGround(IFormFile img)//Metoodo para actualizar la imagen de la compañia 
        {   /*Nota: los logos se guarsan en wwwroot/images/CompanyLogo*/
            try{
                var user = _Sesion.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"); //Se obtiene la data del usuario de la sesión
                int CompanyID = user.FirstOrDefault().CompanyID; 
                string fileName = "Company" + CompanyID + "_logo" + Path.GetExtension(img.FileName); // Se generar el nombre con el cual se guardara el logo
                string filePath = Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo", fileName); //Se genera la ruta del logo en  la caperta root 

                string[] existingFiles = Directory.GetFiles(Path.Combine(iWebHostEnvironment.WebRootPath, "images", "CompanyLogo"), "Company" + CompanyID + "_logo.*"); //Se intenta obtener el logo 
                foreach (string existingFile in existingFiles)
                {
                    System.IO.File.Delete(existingFile);// Se elimina el logo en caso de existir 
                }
                using (var image = new MagickImage(img.OpenReadStream())) // Se inicializa la libreria MagickImage
                {
                    image.Quality = 70; // Se reduce la calidad s un  70%
                    image.Resize(800, 600); // Se cambia la densidad de pixeles 800*600
                    image.Strip();//Se eliminan los metadatos para reducir tamaño de imagen 
                    image.Write(filePath);//Se guarda la imagen
                }
                return true; //En caso de exito se retorna true
            }
            catch (Exception)
            {
                return false;//En caso de fallo se retorna false 
            }            
        }
    }
}
