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
    public class EfGetGenresQuery : IGetGenresQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetGenresQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 1;

        public string Name => "Get genres ef";

        public PaginationResponse<GenreDTO> Execute(GenreSearch search)
        {
            var query = _context.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.GenreName.ToLower().Contains(search.Name.ToLower()));
            }

            return query.Paged<GenreDTO, Genre>(search, _mapper);
        }
    }
}
