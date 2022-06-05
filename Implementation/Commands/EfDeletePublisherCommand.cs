using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeletePublisherCommand : IDeletePubliserCommand
    {
        private readonly BookstoreContext _context;

        public EfDeletePublisherCommand(BookstoreContext context)
        {
            _context = context;
        }

        public int Id => 1;

        public string Name => "soft delete publisher";

        public void Execute(int request)
        {
            if (_context.Publishers.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(Publisher));
            }

            var publisher = _context.Publishers.Find(request);
            publisher.Id = request;

            publisher.IsDeleted = true;
            publisher.IsActive = false;
            publisher.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
