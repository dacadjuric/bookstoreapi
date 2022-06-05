using Application;
using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Querys
{
    public class EfGetBookQuery : IGetBookQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetBookQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Getting book EF";

        public IEnumerable<BookDTO> Execute(int search)
        {
            var query = _context.Books.AsQueryable();

            query = query.Where(b => b.Id == search);

            return query.Select(book => _mapper.Map<BookDTO>(book)).ToList();
        }
    }
}
