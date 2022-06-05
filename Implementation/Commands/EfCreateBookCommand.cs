using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfCreateBookCommand : ICreateBookCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly AddBookValidator _validations;

        public EfCreateBookCommand(BookstoreContext context, IMapper mapper, AddBookValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }

        public int Id => 1;

        public string Name => "Create new book";

        public void Execute(BookDTO request)
        {
            _validations.ValidateAndThrow(request);

            var book = _mapper.Map<Book>(request);

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void AddBookAuthor(BookDTO dto, Book book)
        {
            foreach (var g in dto.GenreIds)
            {
                _context.BookGenres.Add(new BookGenre
                {
                    GenreId = g.Id,
                    BookId = book.Id
                });
            }

            _context.SaveChanges();
        }

        public void AddBookGenre(BookDTO dto, Book book)
        {
            foreach (var a in dto.AuthorIds)
            {
                _context.BookAuthors.Add(new BookAuthor
                {
                    AuthorId = a.Id,
                    BookId = book.Id
                });
            }

            _context.SaveChanges();
        }
    }
}
