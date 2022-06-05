using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using AutoMapper;
using DataAccess;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using static System.Net.Mime.MediaTypeNames;

namespace Implementation.Querys
{
    public class EfGetImagesQuery : IGetImagesQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetImagesQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public PaginationResponse<ImageDTO> Execute(ImageSearch search)
        {
            var query = _context.Images.AsQueryable();

            if (!string.IsNullOrEmpty(search.Alt) || !string.IsNullOrWhiteSpace(search.Alt))
            {
                query = query.Where(x => x.Alt.ToLower().Contains(search.Alt.ToLower()));
            }

            return query.Paged<ImageDTO, Domain.Image>(search, _mapper);
        }
    }
}
