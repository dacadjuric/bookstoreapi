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
    public class EfGetAuthorsQuery : IGetAuthorsQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetAuthorsQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public PaginationResponse<AuthorDTO> Execute(AuthorSearch search)
        {
            var query = _context.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
            }

            return query.Paged<AuthorDTO, Author>(search, _mapper);
        }
    }
}
