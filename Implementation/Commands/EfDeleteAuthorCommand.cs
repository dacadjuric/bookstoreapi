using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteAuthorCommand : IDeleteAuthorCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteAuthorCommand(BookstoreContext context)
        {
            _context = context;
        }
        public int Id => 1;

        public string Name => "soft delete author";

        public void Execute(int request)
        {
            if (_context.Authors.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(Author));
            }

            var author = _context.Authors.Find(request);
            author.Id = request;

            author.IsDeleted = true;
            author.IsActive = false;
            author.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
