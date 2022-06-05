using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Search
{
    public abstract class PagedSearch
    {
        public int PerPage { get; set; } = 3;
        public int Page { get; set; } = 1;
    }
}
