using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfCreateBookGenreCommand : ICreateBookGenreCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfCreateBookGenreCommand(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "create book genre";

        public void Execute(BookGenreDTO request)
        {
            var bg = _mapper.Map<BookGenre>(request);

            _context.BookGenres.Add(bg);
            _context.SaveChanges();
        }
    }
}