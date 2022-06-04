using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Image : Entity
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }//slika ima jednu kjnigu

    }
}
