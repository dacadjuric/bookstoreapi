using Application.DataTransfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class AddImageValidator : AbstractValidator<ImageDTO>
    {
        public AddImageValidator()
        {
            RuleFor(x => x.Alt)
            .NotEmpty()
            .WithMessage("Description for image is required");

            RuleFor(x => x.Src)
            .NotEmpty()
            .WithMessage("Path for image is required");
        }
    }
}
