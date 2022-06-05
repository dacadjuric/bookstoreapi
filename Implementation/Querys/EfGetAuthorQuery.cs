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
    public class EfGetAuthorQuery : IGetAuthorQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetAuthorQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get author ef";

        public IEnumerable<AuthorDTO> Execute(int search)
        {
            var query = _context.Authors.AsQueryable();

            query = query.Where(b => b.Id == search);

            return query.Select(author => _mapper.Map<AuthorDTO>(author)).ToList();
        }
    }
}
