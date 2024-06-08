using Application.DTO;
using Application.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Email
{
    public class SMTPEmailSender : IEmailSender
    {
        public void SendEmail(EmailDTO email)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("djordje.tesa@gmail.com", "bawm paxv qvga jeyr")
            };
            var message = new MailMessage("djordje.tesa@gmail.com", email.SendTo);
            message.Subject = email.Subject;
            message.Body = email.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
