using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfUpdateBookAuthorCommand : IUpdateBookAuthorCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfUpdateBookAuthorCommand(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 1;

        public string Name => "update book author";

        public void Execute(BookAuthorDTO request)
        {
            if (_context.BookAuthors.Find(request) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(BookAuthor));
            }

            var ba = _context.BookAuthors.Find(request.Id);

            _mapper.Map(request, ba);
            _context.SaveChanges();
        }
    }
}
