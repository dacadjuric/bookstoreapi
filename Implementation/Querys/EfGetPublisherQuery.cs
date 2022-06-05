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
    public class EfGetPublisherQuery : IGetPublisherQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetPublisherQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 1;

        public string Name => "Get publisher";

        public IEnumerable<PublisherDTO> Execute(int search)
        {
            var query = _context.Publishers.AsQueryable();

            query = query.Where(p => p.Id == search);

            return query.Select(publisher => _mapper.Map<PublisherDTO>(publisher)).ToList();
        }
    }
}
