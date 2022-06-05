using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdatePublisherValidator : AbstractValidator<PublisherDTO>
    {
        public UpdatePublisherValidator(BookstoreContext context)
        {
            RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");

            RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required");

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Must(name => !context.Publishers.Any(g => g.Name == name))
            .WithMessage("Image with that path already exists.");
        }
    }
}
