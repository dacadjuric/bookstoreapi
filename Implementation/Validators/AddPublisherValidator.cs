using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class AddPublisherValidator : AbstractValidator<PublisherDTO>
    {
        public AddPublisherValidator(BookstoreContext context)
        {
            RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");

            RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required");

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        }
    }
}
