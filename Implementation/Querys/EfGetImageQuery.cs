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
    public class EfGetImageQuery : IGetImageQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetImageQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 1;

        public string Name => "Get image ef";

        public IEnumerable<ImageDTO> Execute(int search)
        {
            var query = _context.Images.AsQueryable();

            query = query.Where(g => g.Id == search);

            return query.Select(image => _mapper.Map<ImageDTO>(image)).ToList();
        }
    }
}
