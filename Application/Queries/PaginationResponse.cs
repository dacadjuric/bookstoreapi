using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public class PaginationResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int pagesCount => (int)Math.Ceiling((float)TotalCount / ItemsPerPage);

        public IEnumerable<T> Items { get; set; }

    }
}
