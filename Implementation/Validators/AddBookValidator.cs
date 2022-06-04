using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class AddBookValidator : AbstractValidator<BookDTO>
    {
        private readonly BookstoreContext _context;
        public AddBookValidator(BookstoreContext context)
        {
            //RuleFor(x => x.Id)
            //    .Must(BookExists)
            //    .WithMessage("B")

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Book title is required")
                .Must(name => !context.Books.Any(g => g.Title == name))
                .WithMessage("Book title must be unique");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .WithMessage("ISBN is required.")
                .Must(isbn => !context.Books.Any(g => g.ISBN == isbn))
                .WithMessage("ISBN must be unique.");

            RuleFor(x => x.TotalPages)
                .NotEmpty()
                .WithMessage("Number of pages is required.")
                .GreaterThan(0)
                .WithMessage("Number of pages can't be 0 or less.");

            RuleFor(x => x.PublishedDate)
                .NotEmpty()
                .WithMessage("Published date is requird.")
                .LessThan(DateTime.Now)
                .WithMessage("Published date can not be in future");
        }

        private bool BookExists(int bookId)
        {
            return _context.Books.Any(x => x.Id == bookId);
        }
    }
}
