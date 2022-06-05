using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Search
{
    public class UseCaseLogSearch : PagedSearch
    {
        public string Text { get; set; }
        public string SearchBy { get; set; }
    }
}
