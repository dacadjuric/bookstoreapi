using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateBookValidator : AbstractValidator<BookDTO>
    {
        private readonly BookstoreContext _context;
        public UpdateBookValidator(BookstoreContext context)
        {
            RuleFor(x => x.Id)
                .Must(BookExists)
                .WithMessage("Book with id {PropertyValue} does not exists.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .Must((dto, y) => !context.Books.Any(p => p.Title == y && p.Id != dto.Id))
                .WithMessage(p => $"Book with the title {p.Title} already exists.");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .WithMessage("ISBN is required.")
                .Must((dto, y) => !context.Books.Any(p => p.ISBN == y && p.Id != dto.Id))
                .WithMessage(p => $"Book with the ISBN {p.ISBN} already exists.");

            RuleFor(x => x.TotalPages)
                .GreaterThan(0)
                .WithMessage("Number of pages must be above 0.");

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
