using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }

        public ICollection<BookAuthor> AuthorBooks { get; set; }
        public ICollection<UseCaseAuthor> UseCaseAuthors { get; set; }
    }
}
