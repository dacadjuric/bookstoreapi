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
    public class EfCreateBookAuthorCommand : ICreateBookAuthorCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfCreateBookAuthorCommand(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "create book author";

        public void Execute(BookAuthorDTO request)
        {
            var ba = _mapper.Map<BookAuthor>(request);

            _context.BookAuthors.Add(ba);
            _context.SaveChanges();
        }
    }
}
