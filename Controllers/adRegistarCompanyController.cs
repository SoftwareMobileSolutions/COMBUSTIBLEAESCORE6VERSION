using COMBUSTIBLEAESCORE.Interfaces;
//using COMBUSTIBLEAESCORE.Models;
//using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;
//using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
//using System.IO;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adRegistarCompanyController : Controller
    {
        private readonly IadRegistarCompany iRegister;
        private string EncryptKey = "$_M_$_BLU3_KNTRL"; //Contraseña para encrptar y descincriptar
        private string Correo_Monitoreo = "analista.monitoreo@sms-open.com";

        public adRegistarCompanyController(IadRegistarCompany _IRegister) {
            iRegister = _IRegister;
        }

        public IActionResult adRegistarCompany()
        {
            return PartialView();
        }

        [HttpPost]//Función para registrar la Company
        public async Task<JsonResult> registerCompany(string Nombre, string Apellido, string Username, string Clave, string Correo, string NombreCompany, string DireccionCompany)
        {
            /*Se intenta registrar la empresa*/
            var data = await iRegister.RegisterCompany(Nombre, Apellido, Username, Clave, Correo, NombreCompany, DireccionCompany);
            
            if (data.FirstOrDefault().bandera == 1) {//Si se creó la empresa de manera correcta, se envian dos correos

                /**************CORREO PARA IDC*********************************************************************************************************************/
                var EncabezadoCorreo_IDC = "Notificación para activación de usuario - " + Username;//Se define el encabezado 
                var URL_Activacion = GetAbsoluteRootUrl() + "/ActivarUsuario/" + Encrypt(Username, EncryptKey);//Se crea el URL de activación de usuario
                var CuerpoCorreo_IDC = "<html><body>" +
                                    "<p>Saludos cordiales.</p>" +
                                    "<p>Se creo la compañía con el nombre <strong>" + NombreCompany + "</strong>  por favor activarla con el siguiente enlace</p>" +
                                    "<ul>" +
                                    "<li>Enlace de activación de usuario: <a href=" + URL_Activacion + ">Click Aquí</a> </li>" +
                                    "</ul>" +
                                    "</body></html>"; //Se define el cuerpo del correo

                var ResultadoCorreoIDC =  await enviarCorreo(Correo_Monitoreo, CuerpoCorreo_IDC, EncabezadoCorreo_IDC);//Se envia el correo
                /*************************************************************************************************************************************************/


                /**************CORREO PARA USUARIO***************************************************************************************************************/


                var EncabezadoCorreo_Usuario = "Notificación de creación de compañía  " + NombreCompany;//Se define el encabezado
                var CuerpoCorreo_Usuario = "<html><body>" +
                                            "<p>Saludos cordiales de parte de Software Mobile Solutions.</p>" +
                                            "<p>Por este medio le comunicamos qué la compañía <strong>" + NombreCompany + "</strong>  ha sido creada exitosamente</p>" +
                                            "<br />" +
                                            "<p>A continuación, encontrará las credenciales para el usuario administrador:</p>" +
                                            "<ul>" +
                                            "<li>Usuario: " + Username + "</li>" +
                                            "<li>Contraseña: " + Clave + "</li>" +
                                            "</ul>" +
                                            "<p>Quedamos a la orden para cualquier consulta.</p>" +

                                            "</body></html>";//Se define el cuerpo de correo 
                var ResultadoCorreoUsuario = await enviarCorreo(Correo, CuerpoCorreo_Usuario, EncabezadoCorreo_Usuario);//Se envía el correo
                /*************************************************************************************************************************************************/
            }

            return Json(data.FirstOrDefault());
            //return Json(new {  });
        }

        protected async Task<bool> enviarCorreo(string correo, string CuerpoCorreo, string EncabezadoCorreo) //Función para enviar correos 
        {
            string SendMailFrom = "smsnotificaciones@sms-open.com";//Se define el correo del cual se enviara el correo
            string SendMailPassword = "Notificac_ones09183$192";//Se define la contraseña del correo del cual se enviara el correo

            MailMessage email = new MailMessage();//Se instancia la clase email
            email.From = new MailAddress(SendMailFrom);//Se define el email de del cual se enviara el correo 
            email.To.Add(correo + "");//Se define al correo de destino
            //email.To.Add("henryhrra@gmail.com");

            /*"Notificaión para activacion de usuario - " + username;*/
            email.Subject = EncabezadoCorreo;//Se define el encabezado del correo 


            email.IsBodyHtml = true;//Se define que se usará una plantilla HTML
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(CuerpoCorreo,null, "text/html");//Se intancia la clase AlternateView y se le genera la plantilla HTML 
            email.AlternateViews.Add(htmlView);//Se añade la plantilla HTML 

            SmtpClient SmtpServer = new SmtpClient("smtp.mydomain.com", 587);//Se define el Mail Server
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.EnableSsl = false;//Se inabilita la verificaión SSl 
            SmtpServer.UseDefaultCredentials = false;

            SmtpServer.Credentials = new NetworkCredential(SendMailFrom, SendMailPassword);//Se añaden las credenciales 

            try
            {
                await SmtpServer.SendMailAsync(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            

        }

        [Route("ActivarUsuario/{encrypted_username}")]//Se  usa route para obtener el usuario encriptado 
        public async Task<IActionResult> ActivarUsuario( string encrypted_username)//Action pata activar el usuario
        {
            return await Task.Run(async () =>
            {
            var UsuarioDesencrioptado = Decrypt(encrypted_username, EncryptKey);//Se desencripta el usuario
            var mensaje = await iRegister.ActivarUsuario(UsuarioDesencrioptado);//Se verifica el resultado

            /*Se verifica cual es el estado el usuario*/
            if (mensaje.First().bandera.Equals(1))//Se activó el usuario
                {
                    return PartialView("~/Views/adRegistarCompany/ActivacionUsuario/ActivacionExitosa.cshtml");
                }
            else//El usuario se encuentra inactivo
                {
                    ViewData["Mensaje"] = mensaje.First().resultado;
                    return PartialView("~/Views/adRegistarCompany/ActivacionUsuario/ActivacionFallida.cshtml");
                }
            });
        }

        private string GetAbsoluteRootUrl()//Funcion para obtener la ruta abosoluta del sitio web
        {
            var request = HttpContext.Request;
            var absoluteUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent(),
                request.PathBase.ToUriComponent());

            return absoluteUri;
        }


        public static string Encrypt(string input, string clave) //Función para encriptar usando el metodo Aes
        {
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            using (Aes aes = Aes.Create())
            {
                aes.Key = claveBytes;
                aes.Mode = CipherMode.ECB; // Modo de cifrado: Electronic Codebook (ECB)
                aes.Padding = PaddingMode.PKCS7; // Relleno: PKCS7

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Base64UrlEncode(encrypted);
            }
        }

        public static string Decrypt(string input, string clave)//Función para desencriptar usando el metodo Aes
        {
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
            byte[] inputBytes = Base64UrlDecode(input);

            using (Aes aes = Aes.Create())
            {
                aes.Key = claveBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decrypted = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Encoding.UTF8.GetString(decrypted);
            }
        }

        private static string Base64UrlEncode(byte[] input)// Función para encriptar en base 64
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        private static byte[] Base64UrlDecode(string input)// Función para desencriptar en base 64
        {
            string base64 = input.Replace('-', '+')
                .Replace('_', '/')
                .PadRight(input.Length + (4 - input.Length % 4) % 4, '=');
            return Convert.FromBase64String(base64);
        }

    }
}
