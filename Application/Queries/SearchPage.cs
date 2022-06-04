using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public abstract class SearchPage
    {
        public int PerPage { get; set; } = 3;
        public int Page { get; set; } = 1;
    }
}
