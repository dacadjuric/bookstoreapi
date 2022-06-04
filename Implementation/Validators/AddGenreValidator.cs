using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class AddGenreValidator : AbstractValidator<GenreDTO>
    {
        public AddGenreValidator(BookstoreContext context)
        {
            RuleFor(x => x.GenreName)
            .NotEmpty()
            .WithMessage("Genre name is required");
        }
    }
}
