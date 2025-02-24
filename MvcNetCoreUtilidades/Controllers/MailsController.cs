using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        //recuperamos las credenciales del appsettings -> con inyeccion
        private IConfiguration configuration;

        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            //debemos indicar el from -> de que cuenta viente el correo (la nuestra que hemos puesto en appsettings)
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User"); //recuperamos el user
            mail.From = new MailAddress(user);
            //los destinatarios (to:) son una coleccion
            mail.To.Add(to);
            //asunto
            mail.Subject = asunto;
            mail.Body = mensaje;
            //tiene mas propiedades
            //mail.IsBodyHtml = true; //para poder meter html en el body, interpreta el codigo.
            mail.Priority = MailPriority.Normal; //prioridad -> alta, normal o baja

            //recuoeramos valores de appsettings
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Server:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Server:Ssl");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Server:DefaultCredentials");
            //cremaos la clase servidor smtp y la instaciamos
            SmtpClient smtCliente = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = ssl,
                UseDefaultCredentials = defaultCredentials
            };
            //creamos las credenciales para el mail -> hay mails que van sin @ (habria que mirar la documentacion)
            NetworkCredential credentials = new NetworkCredential(user, password);
            smtCliente.Credentials = credentials;
            await smtCliente.SendMailAsync(mail);
            ViewData["MENSAJE"] = "Mail enviado correctamente";
            //devolvemos la vista
            return View();
        }
    }
}
