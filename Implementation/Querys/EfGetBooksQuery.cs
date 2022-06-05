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
    public class EfGetBooksQuery : IGetBooksQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetBooksQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get books EF";

        public PaginationResponse<BookDTO> Execute(BookSearch search)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.Name.ToLower()));
            }

            return query.Paged<BookDTO, Book>(search, _mapper);
        }
    }
}
