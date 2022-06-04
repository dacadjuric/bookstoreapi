using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public int TotalPages { get; set; }
        public int ISBN { get; set; }
        public DateTime PublishedDate { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }

        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
