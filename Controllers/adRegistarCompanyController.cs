using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class adRegistarCompanyController : Controller
    {
        private readonly IadRegistarCompany iRegister;

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
            var Fullname = Nombre + " " + Apellido;
            await enviarCorreo(NombreCompany, Fullname, Username, Clave, Correo);
            return Json(data.FirstOrDefault());
            //return Json(new { });
        }

        protected async Task enviarCorreo(string company,string name,string username,string password,string correo) {
            string SendMailFrom = "smsnotificaciones@sms-open.com";
            string SendMailPassword = "Notificac_ones09183$192";

            MailMessage email = new MailMessage();
            email.From = new MailAddress(SendMailFrom);
            email.To.Add("henry.herrera@sms-open.com");
            email.Subject = "Notificaión para activacion de usuario - " + username;

            email.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
            "<html><body>" +
            "<p>Saludos cordiales.</p>" +
            "<p>Se creó la compañía <strong>" + company + "</strong>  con la siguiente información </p>" +
            "<ul>" +
            "<li>Usuario: " + username + "</li>" +
            "<li>Correo: " + correo + "</li>" +
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
    }


}
