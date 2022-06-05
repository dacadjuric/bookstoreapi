using Application.Commands;
using Application.DataTransfer;
using Application.Email;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfRegistrationCommand : IRegistrationCommand
    {
        private readonly BookstoreContext _context;
        private readonly RegistrationValidator _validator;
        private readonly ISendEmail _sender;

        public EfRegistrationCommand(BookstoreContext context, RegistrationValidator validator, ISendEmail send)
        {
            _context = context;
            _validator = validator;
            _sender = send;
        }

        public int Id => 12;

        public string Name => "Registartion";

        public void Execute(RegistrationDTO request)
        {
            _validator.ValidateAndThrow(request);

            _context.Authors.Add(new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            });

            _context.SaveChanges();

            _sender.Send(new SendEmailDTO
            {
                Email = "<h3>Successfull registration!</h3>",
                SendTo = request.Email,
                Subject = "Registration"
            });
        }
    }
}
