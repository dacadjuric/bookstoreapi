using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Email
{
    public interface ISendEmail
    {
        void Send(SendEmailDTO dto);
    }

    public class SendEmailDTO
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
    }
}
