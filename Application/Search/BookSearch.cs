using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Search
{
    public class BookSearch : PagedSearch
    {
        public string Name { get; set; }
    }
}
