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
    public class EfGetPublishersQuery : IGetPublishersQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetPublishersQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public PaginationResponse<PublisherDTO> Execute(PublisherSearch search)
        {
            var query = _context.Publishers.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            return query.Paged<PublisherDTO, Publisher>(search, _mapper);
        }
    }
}
