using Application.Commands;
using Application.Exceptions;
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
    public class EfDeleteBookCommand : IDeleteBookCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteBookCommand(BookstoreContext context)
        {
            _context = context;
        }

        public int Id => 1;

        public string Name => "Soft delete book";

        public void Execute(int request)
        {

            if (_context.Books.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(Book));
            }

            var book = _context.Books.Find(request);
            book.Id = request;

            book.IsDeleted = true;
            book.IsActive = false;
            book.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
