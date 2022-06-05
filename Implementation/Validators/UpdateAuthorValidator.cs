using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateAuthorValidator : AbstractValidator<AuthorDTO>
    {
        public UpdateAuthorValidator(BookstoreContext context)
        {
            
            RuleFor(x => x.FirstName)
           .NotEmpty()
           .WithMessage("First name is required.");

            RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required")
                .Must(username => !context.Authors.Any(g => g.Username == username))
                .WithMessage("Username already exists.")
                .MaximumLength(25)
                .WithMessage("Max length is 25 characters,");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .Must(email => !context.Authors.Any(g => g.Email == email))
                .WithMessage("Email must be unique.")
                .EmailAddress();

            RuleFor(x => x.Telephone)
                .Must(tel => !context.Authors.Any(g => g.Telephone == tel))
                .WithMessage("Telephone already exists.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+?><.,])(?=.{4,})")
                .WithMessage("Password must have lower case, uppercase, number, special character and minimum length of 4 characters.");

        }
    }
}
