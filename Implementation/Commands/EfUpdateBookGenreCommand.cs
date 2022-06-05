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
    public class EfUpdateBookGenreCommand : IUpdateBookGenreCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfUpdateBookGenreCommand(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 1;

        public string Name => "update book genre";

        public void Execute(BookGenreDTO request)
        {
            if (_context.BookGenres.Find(request) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(BookGenre));
            }

            var bg = _context.BookGenres.Find(request.Id);

            _mapper.Map(request, bg);
            _context.SaveChanges();
        }
    }
}
