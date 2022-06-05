using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteBookAuthorCommand : IDeleteBookAuthorCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteBookAuthorCommand(BookstoreContext context)
        {
            _context = context;
        }
        public int Id => 1;

        public string Name => "soft delete book author";

        public void Execute(int request)
        {
            if (_context.BookAuthors.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(BookAuthor));
            }

            _context.Remove(_context.BookAuthors.Find(request));
            _context.SaveChanges();
        }
    }
}
