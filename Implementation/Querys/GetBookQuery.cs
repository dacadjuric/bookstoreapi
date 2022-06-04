using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Querys
{
    public class GetBookQuery : IGetBookQuery
    {
        private readonly BookstoreContext _context;

        public GetBookQuery(BookstoreContext context)
        {
            _context = context;
        }
        public int Id => 4;

        public string Name => "Group search";

        public PaginationResponse<BookDTO> Execute(BookSearch search)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.Name.ToLower()));

            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PaginationResponse<BookDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new BookDTO
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList()
            };

            return response;
        }
    }
}
