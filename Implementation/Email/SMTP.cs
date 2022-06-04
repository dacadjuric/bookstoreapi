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
        private readonly string _fromEmail;
        private readonly string _fromPassword;

        public SMTP(string fromEmail, string fromPassword)
        {
            _fromEmail = fromEmail;
            _fromPassword = fromPassword;
        }

        public void Send(SendEmailDTO dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 5000,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_fromEmail, _fromPassword)
            };

            var message = new MailMessage(_fromPassword, dto.SendTo);
            message.Subject = dto.Subject;
            message.Body = dto.Email;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
