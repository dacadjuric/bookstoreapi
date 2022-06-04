using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Genre : Entity
    {
        public string GenreName { get; set; }

        public ICollection<BookGenre> GenreBooks { get; set; }
    }
}
