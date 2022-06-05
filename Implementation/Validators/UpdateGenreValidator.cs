using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateGenreValidator : AbstractValidator<GenreDTO>
    {
        public UpdateGenreValidator(BookstoreContext context)
        {
            RuleFor(x => x.GenreName)
            .NotEmpty()
            .WithMessage("Genre name is required")
            .Must(name => !context.Genres.Any(g => g.GenreName == name))
            .WithMessage("Genre must be unique"); ;
        }
    }
}
