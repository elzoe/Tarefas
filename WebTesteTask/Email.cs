using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace WebTask
{
    public class Email
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (!string.IsNullOrWhiteSpace(message.Destination) && !string.IsNullOrWhiteSpace(message.Subject) && !string.IsNullOrWhiteSpace(message.Body))
            {
                var emailCredencial = ConfigurationManager.AppSettings.Get("EmailCredencial");
                var emailSenha = ConfigurationManager.AppSettings.Get("EmailSenha");
                var emailHost = ConfigurationManager.AppSettings.Get("EmailHostSmtp");
                var emailSmtpPort = int.Parse(ConfigurationManager.AppSettings.Get("EmailHostPort") ?? "0");

                SmtpClient client = new SmtpClient();
                client.Port = emailSmtpPort;
                client.Host = emailHost;
                client.Timeout = 24000;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailCredencial, emailSenha);
                var mailMessage = new MailMessage();

                mailMessage.To.Add(message.Destination);
                mailMessage.Subject = message.Subject;
                mailMessage.Body = message.Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(emailCredencial);

                return client.SendMailAsync(mailMessage);
            }
            else
                return null;
        }
    }

    public class IdentityMessage {

        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}