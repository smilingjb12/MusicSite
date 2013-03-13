using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace MusicSite.Models
{
    public static class EmailSender
    {
        public static void SendActivationCode(string userEmail, string activationCode)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("musicnet555@gmail.com");
            mail.To.Add(userEmail);
            mail.Subject = "Activation code for Music Net";
            string activationLink = string.Format("http://localhost:12919/Account/Activate?code={0}", activationCode);
            mail.Body = string.Format("Follow this link to activate your account: {0}", activationLink);

            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential("musicnet555@gmail.com", "sunnyshores");
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
        }
    }
}