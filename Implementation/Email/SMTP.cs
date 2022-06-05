using Application.DataTransfer;
using Application.Email;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Implementation.Email
{
    public class SMTP : ISendEmail
    {
        public void Send(SendEmailDTO dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("example@gmail.com", "sifra123")
            };

            var message = new MailMessage("example@gmail.com", dto.SendTo);
            message.Subject = dto.Subject;
            message.Body = dto.Email;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
