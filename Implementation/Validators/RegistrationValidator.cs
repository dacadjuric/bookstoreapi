using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationDTO>
    {
        public RegistrationValidator(BookstoreContext context)
        {
            RuleFor(x => x.FirstName).NotEmpty();
           
            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
           
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4);
            
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(4)
                .Must(x => !context.Authors.Any(author => author.Username == x))
                .WithMessage("Username already exists.");

        }
    }
    }
