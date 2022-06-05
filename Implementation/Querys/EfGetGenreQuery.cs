using Application.DataTransfer;
using Application.Queries;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Querys
{
    public class EfGetGenreQuery : IGetGenreQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetGenreQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get genre EF";

        public IEnumerable<GenreDTO> Execute(int search)
        {
            var query = _context.Genres.AsQueryable();

            query = query.Where(g => g.Id == search);

            return query.Select(genre => _mapper.Map<GenreDTO>(genre)).ToList();
        }
    }
}
