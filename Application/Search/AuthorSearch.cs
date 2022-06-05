using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Search
{
    public class AuthorSearch : PagedSearch
    {
        public string Username { get; set; }
    }
}
