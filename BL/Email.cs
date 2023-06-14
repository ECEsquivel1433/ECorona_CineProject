using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{
    public class Email
    {
        public static ML.Result SendEmail(ML.Email email1)
        {
            ML.Result result = new ML.Result();
            try
            {
                
                MailMessage mailMessage = new MailMessage(email1.EmailOrigen, email1.EmailDirection, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");
                mailMessage.IsBodyHtml = true;
                string contenidoHTML = System.IO.File.ReadAllText(email1.EmailRoute);
                
                mailMessage.Body = contenidoHTML;
                string url = email1.UrlDomain + HttpUtility.UrlEncode(email1.EmailDirection);
                mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(email1.EmailOrigen, email1.EmailPassword);

                smtpClient.Send(mailMessage);
                smtpClient.Dispose();

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Ex= ex;
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar el usuario" + ex;
            }
            return result;
        }
    }
}
