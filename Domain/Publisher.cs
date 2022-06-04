using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Publisher : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
