using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class BookDTO
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public int TotalPages { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }

        public IEnumerable<AuthorIds> AuthorIds { get; set; } = new List<AuthorIds>();
        public IEnumerable<GenreIds> GenreIds { get; set; } = new List<GenreIds>();
    }

    public class AuthorIds
    {
        public int Id { get; set; }
    }

    public class GenreIds
    {
        public int Id { get; set; }
    }
}
