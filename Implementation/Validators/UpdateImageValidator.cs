using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateImageValidator : AbstractValidator<ImageDTO>
    {
        public UpdateImageValidator(BookstoreContext context)
        {
            RuleFor(x => x.Alt)
            .NotEmpty()
            .WithMessage("Description for image is required");

            RuleFor(x => x.Src)
            .NotEmpty()
            .WithMessage("Path for image is required")
            .Must(src => !context.Images.Any(g => g.Src == src))
            .WithMessage("Image with that path already exists.");
        }
    }
}
