using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteBookGenreCommand : IDeleteBookGenreCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteBookGenreCommand(BookstoreContext context)
        {
            _context = context;
        }
        public int Id => 1;

        public string Name => "soft delete book genre";

        public void Execute(int request)
        {
            if (_context.BookGenres.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(BookGenre));
            }

            _context.Remove(_context.BookGenres.Find(request));
            _context.SaveChanges();
        }
    }
}
