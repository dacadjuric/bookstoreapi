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
}
