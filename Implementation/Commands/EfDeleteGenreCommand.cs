using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteGenreCommand : IDeleteGenreCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteGenreCommand(BookstoreContext context)
        {
            _context = context;
        }
        public int Id => 1;

        public string Name => "soft delete genre";

        public void Execute(int request)
        {
            if (_context.Genres.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(Genre));
            }

            var genre = _context.Genres.Find(request);
            genre.Id = request;

            genre.IsDeleted = true;
            genre.IsActive = false;
            genre.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
