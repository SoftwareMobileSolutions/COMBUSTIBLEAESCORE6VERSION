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
//using System.IO;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adRegistarCompanyController : Controller
    {
        private readonly IadRegistarCompany iRegister;
        private string EncryptKey = "$_M_$_BLU3_KNTRL";

        public adRegistarCompanyController(IadRegistarCompany _IRegister) {
            iRegister = _IRegister;
        }

        public IActionResult adRegistarCompany()
        {
            return PartialView();
        }

        public async Task<JsonResult> registerCompany(string Nombre, string Apellido, string Username, string Clave, string Correo, string NombreCompany, string DireccionCompany)
        {
            var data = await iRegister.RegisterCompany(Nombre, Apellido, Username, Clave, Correo, NombreCompany, DireccionCompany);            
            //var Fullname = Nombre + " " + Apellido;
            await enviarCorreo(NombreCompany, Username, Correo);
            return Json(data.FirstOrDefault());
            //return Json(new {  });
        }

        protected async Task enviarCorreo(string NombreCompany/*, string name*/, string username, string correo) {
            string SendMailFrom = "smsnotificaciones@sms-open.com";
            string SendMailPassword = "Notificac_ones09183$192";

            var URL_Activacion = GetAbsoluteRootUrl() + "/ActivarUsuario/" + Encrypt(username, EncryptKey);

            MailMessage email = new MailMessage();
            email.From = new MailAddress(SendMailFrom);
            email.To.Add("henry.herrera@sms-open.com");
            email.Subject = "Notificaión para activacion de usuario - " + username;

            email.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
            "<html><body>" +
            "<p>Saludos cordiales.</p>" +
            "<p>Se creó la compañía <strong>" + NombreCompany + "</strong>  con la siguiente información </p>" +
            "<ul>" +
            "<li>Usuario: " + username + "</li>" +
            "<li>Correo: " + correo + "</li>" +
            "<li>Enlace de activacion de usuario: <a href="+ URL_Activacion + ">Click Aquí</a> </li>" +
            "</ul>" +
            "<p>La cual está en espera de que su usuario sea activado.</p>" +
            "</body></html>",
            null, "text/html");
            email.AlternateViews.Add(htmlView);

            SmtpClient SmtpServer = new SmtpClient("smtp.mydomain.com", 587);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;

            SmtpServer.Credentials = new NetworkCredential(SendMailFrom, SendMailPassword);
            SmtpServer.Send(email);
        }

        [Route("ActivarUsuario/{encrypted_username}")]
        public async Task<IActionResult> ActivarUsuario( string encrypted_username)
        {
            return await Task.Run(async () =>
            {
            var UsuarioDesencrioptado = Decrypt(encrypted_username, EncryptKey);
            var mensaje = await iRegister.ActivarUsuario(UsuarioDesencrioptado);

            if (mensaje.First().bandera.Equals(1))
                {
                    return PartialView("~/Views/adRegistarCompany/ActivacionUsuario/ActivacionExitosa.cshtml");
                }
            else
                {
                    ViewData["Mensaje"] = mensaje.First().resultado;
                    return PartialView("~/Views/adRegistarCompany/ActivacionUsuario/ActivacionFallida.cshtml");
                }
            });
        }

        private string GetAbsoluteRootUrl()
        {
            var request = HttpContext.Request;
            var absoluteUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent(),
                request.PathBase.ToUriComponent());

            return absoluteUri;
        }


        public static string Encrypt(string input, string clave)
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

        public static string Decrypt(string input, string clave)
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

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        private static byte[] Base64UrlDecode(string input)
        {
            string base64 = input.Replace('-', '+')
                .Replace('_', '/')
                .PadRight(input.Length + (4 - input.Length % 4) % 4, '=');
            return Convert.FromBase64String(base64);
        }

    }
}
