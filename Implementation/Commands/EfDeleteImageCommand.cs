using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteImageCommand : IDeleteImageCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteImageCommand(BookstoreContext context)
        {
            _context = context;
        }
        public int Id => 1;

        public string Name => "soft delete image";

        public void Execute(int request)
        {
            if (_context.Images.Find(request) == null)
            {
                throw new EntityNotFoundException(request, typeof(Image));
            }

            var image = _context.Images.Find(request);
            image.Id = request;

            image.IsDeleted = true;
            image.IsActive = false;
            image.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
