using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class SendEmailDTO
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
    }
}
